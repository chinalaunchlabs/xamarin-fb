using System;
using Facebook.CoreKit;
using Wiggin.Facebook.iOS;
using System.Threading.Tasks;
using Foundation;

[assembly:Xamarin.Forms.Dependency(typeof(iOSGraphRequest))]
namespace Wiggin.Facebook.iOS
{
	public class iOSGraphRequest: IGraphRequest
	{
		public string Path { get; set; }
		public string HttpMethod { get; set; }
		public string Version { get; set; }
		public IAccessToken AccessToken {
			get {
				return new iOSAccessToken (_token);
			}
		}

		private AccessToken _token;
		private GraphRequest _request;
		private GraphRequestConnection _connection;

		public IGraphRequest NewRequest(IAccessToken token, string path, string parameters, string httpMethod = default(string), string version = default(string)) {
			_token = (token as iOSAccessToken).ToNative ();
			Path = path;
			HttpMethod = httpMethod;
			Version = version;

			// Assume parameters are for the "field" key
			// TODO: See if there are other key values for the FB SDK.
			Foundation.NSDictionary dict = new Foundation.NSDictionary("fields", parameters);

			_request = new GraphRequest (Path, dict, _token.TokenString, HttpMethod, Version);

			return this;
		}

		public Task<IGraphResponse> ExecuteAsync() {
			TaskCompletionSource<IGraphResponse> tcs = new TaskCompletionSource<IGraphResponse> ();

			var handler = new GraphRequestHandler (( connection, result, error ) => {
				System.Diagnostics.Debug.WriteLine(result);
				System.Diagnostics.Debug.WriteLine(result.GetType());
				tcs.SetResult(new iOSGraphResponse((NSMutableDictionary)result));
			});
			_connection = new GraphRequestConnection ();
			_connection.AddRequest (_request, handler);

			_connection.LoadingFinished += (object sender, EventArgs e) => {
				System.Diagnostics.Debug.WriteLine("Loading finished.");
			};

			_connection.BodyDataSent += (sender, e) => {
				System.Diagnostics.Debug.WriteLine("Body data sent");
			};

			_connection.Failed += (sender, e) => {
				System.Diagnostics.Debug.WriteLine("Request failed");
				tcs.SetCanceled();
			};

			_connection.WillBeginLoading += (sender, e) => {
				System.Diagnostics.Debug.WriteLine("Will begin loading");
			};

			_connection.Start ();

			return tcs.Task;
		}

	}
}


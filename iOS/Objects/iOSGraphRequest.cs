using System;
using Facebook.CoreKit;
using Wiggin.Facebook.iOS;
using System.Threading.Tasks;
using Foundation;
using System.Collections.Generic;

[assembly:Xamarin.Forms.Dependency(typeof(iOSGraphRequest))]
namespace Wiggin.Facebook.iOS
{
	public class iOSGraphRequest: IGraphRequest
	{
		public string Path { get; set; }
		public string HttpMethod { get; set; }
		public string Version { get; set; }

		private AccessToken _token;
		private GraphRequest _request;
		private GraphRequestConnection _connection;

		public IGraphRequest NewRequest(FbAccessToken token, string path, Dictionary<string,string> parameters, string httpMethod = default(string), string version = default(string)) {

			if (parameters != null) {
				var dict = new NSMutableDictionary<NSString, NSString> ();
				foreach (var key in parameters.Keys) {
					dict.Add (new NSString(key), new NSString(parameters [key]));
				}
				Initialize (token, path, dict, httpMethod, version);
			} else {
				Initialize (token, path, null, httpMethod, version);
			}

			return this;

		}

//		public IGraphRequest NewRequest(IAccessToken token, string path, string parameters, string httpMethod = default(string), string version = default(string)) {
////			_token = (token as iOSAccessToken).ToNative ();
////			Path = path;
////			HttpMethod = httpMethod;
////			Version = version;
//
//			// Assume parameters are for the "field" key
//			// TODO: Let parameters be a Dictionary<string,string> bc there are more keys.
//			Foundation.NSDictionary dict = new Foundation.NSDictionary("fields", parameters);
//
////			_request = new GraphRequest (Path, dict, _token.TokenString, HttpMethod, Version);
//
//			Initialize (token, path, dict, httpMethod, version);
//
//			return this;
//		}

		public Task<IGraphResponse> ExecuteAsync() {
			TaskCompletionSource<IGraphResponse> tcs = new TaskCompletionSource<IGraphResponse> ();

			var handler = new GraphRequestHandler (( connection, result, error ) => {
//				System.Diagnostics.Debug.WriteLine(result);
				tcs.SetResult(new iOSGraphResponse((NSDictionary)result));
			});
			_connection = new GraphRequestConnection ();
			_connection.AddRequest (_request, handler);

			_connection.Failed += (sender, e) => {
				System.Diagnostics.Debug.WriteLine("Request failed");
				tcs.SetCanceled();
			};

			_connection.Start ();

			return tcs.Task;
		}

		private void Initialize(FbAccessToken token, string path, Foundation.NSDictionary parameters, string httpMethod = default(string), string version = default(string)) {
			_token = token.ToNative ();
			Path = path;
			HttpMethod = httpMethod;
			Version = version;

			_request = new GraphRequest (Path, parameters == null ? null : parameters, _token.TokenString, HttpMethod, Version);
		}
	}
}


using System;
using Xamarin.Facebook;
using Wiggin.Facebook.Droid;
using System.Threading.Tasks;

[assembly:Xamarin.Forms.Dependency(typeof(GraphRequestService))]
namespace Wiggin.Facebook.Droid
{
	public class GraphRequestService: IGraphRequest
	{
		private AccessToken _token;
		public AccessToken AccessToken { 
			get { return _token; }
			set { _token = value; }
		}

		private string _graphPath;
		public string GraphPath { 
			get { return _graphPath; }
			set { _graphPath = value; }
		}

		private GraphRequest _graphRequest;

		public void Initialize(IAccessToken token, string path) {
			AccessToken = AccessToken.CurrentAccessToken;
			GraphPath = path;
			_graphRequest = new GraphRequest (AccessToken, path);

		}

//		public async Task<string> NewMeRequest() {
////			_graphRequest.
////			var test = new GraphRequestAsyncTask
//		}

//		private AccessToken ConvertToken(DroidAccessToken token) {
//			var _access = new AccessToken ();
//			_access.ApplicationId = token.ApplicationId;
//			_access.DeclinedPermissions = token.DeclinedPermissions;
//			_access.Expires = 
//		}
	}
}


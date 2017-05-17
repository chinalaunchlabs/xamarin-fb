using System;
using Wiggin.Facebook.iOS;
using System.Threading.Tasks;
using Facebook.CoreKit;
using Facebook.LoginKit;
using MonoTouch.Dialog;
using UIKit;
using System.Linq;

[assembly:Xamarin.Forms.Dependency(typeof(FacebookLoginService))]
namespace Wiggin.Facebook.iOS
{
	public class FacebookLoginService: IFacebookLogin
	{
		public async Task<FbAccessToken> LogIn(string[] permissions) {
			System.Diagnostics.Debug.WriteLine ("Hello from iOS-Land!");
			TaskCompletionSource<FbAccessToken> tcs = new TaskCompletionSource<FbAccessToken> ();

			var loginManager = new LoginManager ();
			loginManager.LogOut();
			loginManager.LoginBehavior = LoginBehavior.Web;
			LoginManagerLoginResult result;

			if (permissions.Contains ("publish_actions")) {
				result = await loginManager.LogInWithPublishPermissionsAsync (permissions, null);
			} else {
				result = await loginManager.LogInWithReadPermissionsAsync (permissions, null);
			}

			if (result.IsCancelled) {
				tcs.SetCanceled ();
			} else {
				tcs.SetResult (AccessToken.CurrentAccessToken.ToForms());
			}
				
			return await tcs.Task;
		}

		public bool IsLoggedIn() {
			return AccessToken.CurrentAccessToken != null;
		}

		public FbAccessToken GetAccessToken() {
			return AccessToken.CurrentAccessToken.ToForms();
		}

		public void Logout() {
			var loginManager = new LoginManager ();
			loginManager.LogOut ();
		}
	}
}


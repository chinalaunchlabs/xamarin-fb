using System;
using Wiggin.Facebook.iOS;
using System.Threading.Tasks;
using Facebook.CoreKit;
using Facebook.LoginKit;
using MonoTouch.Dialog;
using UIKit;

[assembly:Xamarin.Forms.Dependency(typeof(FacebookLoginService))]
namespace Wiggin.Facebook.iOS
{
	public class FacebookLoginService: IFacebookLogin
	{
		Profile profile;

		public async Task<IAccessToken> LogIn(string[] permissions) {
			TaskCompletionSource<IAccessToken> tcs = new TaskCompletionSource<IAccessToken> ();

			var loginManager = new LoginManager ();
			loginManager.LoginBehavior = LoginBehavior.SystemAccount;
			LoginManagerLoginResult result = await loginManager.LogInWithReadPermissionsAsync (permissions, null);

			if (result.IsCancelled) {
				tcs.SetCanceled ();
				System.Diagnostics.Debug.WriteLine ("Canceled");
			} 

			// Wait for profile to change before setting the token
			Profile.Notifications.ObserveDidChange ((sender, e) => {

				if (e.NewProfile == null)
					return;

				profile = e.NewProfile;

				tcs.SetResult(new iOSAccessToken(AccessToken.CurrentAccessToken));

			});

//			loginManager.Init();
//			loginManager.LogInWithReadPermissions (permissions, UIApplication.SharedApplication.KeyWindow.RootViewController, (result, error) => {
//				System.Diagnostics.Debug.WriteLine("T____T");
//				// Handle if something went wrong with the request
//				if (error != null) {
//					System.Diagnostics.Debug.WriteLine(error.Description);
//					tcs.SetResult(new iOSAccessToken(AccessToken.CurrentAccessToken));
//					return;
//				}
//
//				// Handle if the user cancelled the request
//				if (result.IsCancelled) {
//					System.Diagnostics.Debug.WriteLine("The request was canceled");
//					tcs.SetCanceled();
//					return;
//				}
//
//				// Do your magic if the request was successful
//				tcs.SetResult(new iOSAccessToken(AccessToken.CurrentAccessToken));
//			});
//			UIWindow window = UIApplication.SharedApplication.KeyWindow;
//			UIViewController viewController = window.RootViewController;
//
//			var loginViewController = new FacebookLoginViewController(permissions);
//
//			FacebookLoginViewController.OnFacebookLoginSuccess += () => {
//				tcs.SetResult(new iOSAccessToken(AccessToken.CurrentAccessToken));
//				viewController.DismissViewController(true, null);
//			};
//			FacebookLoginViewController.OnFacebookLoginError += () => {
//				tcs.SetResult(new iOSAccessToken(AccessToken.CurrentAccessToken));
//				System.Diagnostics.Debug.WriteLine("Error logging in");
//				viewController.DismissViewController(true, null);
//			};
//			FacebookLoginViewController.OnFacebookLoginCancel += () => {
//				tcs.SetCanceled();
//				System.Diagnostics.Debug.WriteLine("Task was canceled");
//				viewController.DismissViewController(true, null);
//			};
//
//			viewController.PresentViewController (loginViewController, true, null);

			return await tcs.Task;
		}

		public bool IsLoggedIn() {
			return true;
		}

		public IAccessToken GetAccessToken() {
			return new iOSAccessToken (AccessToken.CurrentAccessToken);
		}


		public IProfile FetchProfile () {
			if (profile != null)
				return new iOSProfile (profile);
			else
				return null;
		}
	}
}


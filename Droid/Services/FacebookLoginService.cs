using System;
using Wiggin.Facebook;
using Wiggin.Facebook.Droid;
using System.Threading.Tasks;
using Android.App;
using Xamarin.Forms;
using Android.Content;
using Xamarin.Facebook;
using System.Collections.Generic;
using Android.OS;

[assembly:Xamarin.Forms.Dependency(typeof(FacebookLoginService))]
namespace Wiggin.Facebook.Droid
{
	public class FacebookLoginService: global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IFacebookLogin
	{
		private DroidAccessToken _accessToken = null;

		public Task<IAccessToken> LogIn(string[] permissions) {
			TaskCompletionSource<IAccessToken> tcs = new TaskCompletionSource<IAccessToken> ();

			FacebookLoginActivity.OnFacebookLoginSuccess += () => {
				_accessToken = new DroidAccessToken(AccessToken.CurrentAccessToken);
				tcs.SetResult(_accessToken);
			};

			FacebookLoginActivity.OnFacebookLoginCancel += () => {
				tcs.SetCanceled();
			};

			FacebookLoginActivity.OnFacebookLoginError += () => {
				_accessToken = new DroidAccessToken(AccessToken.CurrentAccessToken);
				tcs.SetResult(_accessToken);
			};


			var activity = Forms.Context as Activity;
			var myIntent = new Intent (activity, typeof(FacebookLoginActivity));
			myIntent.PutExtra("permissions", permissions);
			activity.StartActivityForResult (myIntent, 0);

			return tcs.Task;
		}

		public bool IsLoggedIn() {
			return _accessToken != null;
		}

		public IAccessToken GetAccessToken() {
			if (!IsLoggedIn ())
				return null;
			return _accessToken;
		}

		public IProfile FetchProfile() {
			return new DroidProfile (Profile.CurrentProfile);
		}
	}
}


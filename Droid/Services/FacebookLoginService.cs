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
using System.Threading;

[assembly:Xamarin.Forms.Dependency(typeof(FacebookLoginService))]
namespace Wiggin.Facebook.Droid
{
	public class FacebookLoginService: global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IFacebookLogin
	{
		private DroidAccessToken _accessToken = null;
		private TaskCompletionSource<IAccessToken> tcs;

		public Task<IAccessToken> LogIn(string[] permissions) {
			tcs = new TaskCompletionSource<IAccessToken> ();

			FacebookLoginActivity.OnFacebookLoginCancel += FacebookLoginActivity_OnFacebookLoginCancel;
			FacebookLoginActivity.OnFacebookLoginSuccess += FacebookLoginActivity_OnFacebookLoginSuccess;
			FacebookLoginActivity.OnFacebookLoginError += FacebookLoginActivity_OnFacebookLoginError;

			var activity = Forms.Context as Activity;
			var myIntent = new Intent (activity, typeof(FacebookLoginActivity));
			myIntent.PutExtra("permissions", permissions);
	
			activity.StartActivityForResult (myIntent, 0);

			return tcs.Task;
		}

		void FacebookLoginActivity_OnFacebookLoginError ()
		{
			_accessToken = new DroidAccessToken(AccessToken.CurrentAccessToken);
			tcs.SetResult(_accessToken);
			FacebookLoginActivity.OnFacebookLoginError -= FacebookLoginActivity_OnFacebookLoginError;
		}

		void FacebookLoginActivity_OnFacebookLoginSuccess ()
		{
			_accessToken = new DroidAccessToken(AccessToken.CurrentAccessToken);
			tcs.SetResult(_accessToken);
			FacebookLoginActivity.OnFacebookLoginSuccess -= FacebookLoginActivity_OnFacebookLoginSuccess;
		}

		void FacebookLoginActivity_OnFacebookLoginCancel ()
		{
			tcs.SetCanceled();
			FacebookLoginActivity.OnFacebookLoginCancel -= FacebookLoginActivity_OnFacebookLoginCancel;
		}

		public bool IsLoggedIn() {
			return _accessToken != null;
		}

		public IAccessToken GetAccessToken() {
			if (!IsLoggedIn ())
				return null;
			return _accessToken;
		}

		private void LoginSuccessCallback () {
			_accessToken = new DroidAccessToken(AccessToken.CurrentAccessToken);
			System.Diagnostics.Debug.WriteLine("Facebook login success");
			tcs.SetResult(_accessToken);
		}
	}
}


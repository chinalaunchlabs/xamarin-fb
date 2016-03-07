
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;

[assembly: MetaData("com.facebook.sdk.ApplicationId", Value = "@string/facebook_app_id")]
namespace Wiggin.Facebook.Droid
{
	[Activity (Label = "")]			
	public class FacebookLoginActivity : Activity
	{
		public delegate void FacebookDelegate ();
		public static event FacebookDelegate OnFacebookLoginSuccess;
		public static event FacebookDelegate OnFacebookLoginError;
		public static event FacebookDelegate OnFacebookLoginCancel;

		ICallbackManager callbackManager;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			callbackManager = CallbackManagerFactory.Create ();

			var loginCallback = new FacebookCallback<LoginResult> {
				HandleSuccess = loginResult => {
					OnFacebookLoginSuccess();	/// raise event
					this.Finish ();
				},
				HandleCancel = () => {
					OnFacebookLoginCancel();	// raise event
					this.Finish();
				},
				HandleError = loginError => {
					OnFacebookLoginError();		// raise event
					this.Finish();
				}
			};

			LoginManager.Instance.RegisterCallback (callbackManager, loginCallback);

			string[] PERMISSIONS = Intent.GetStringArrayExtra ("permissions");
			LoginManager.Instance.LogInWithReadPermissions (this, PERMISSIONS);
		
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			callbackManager.OnActivityResult (requestCode, (int)resultCode, data);
		}
	}

	class FacebookCallback<TResult> : Java.Lang.Object, IFacebookCallback where TResult : Java.Lang.Object
	{
		public Action HandleCancel { get; set; }
		public Action<FacebookException> HandleError { get; set; }
		public Action<TResult> HandleSuccess { get; set; }

		public void OnCancel ()
		{
			var c = HandleCancel;
			if (c != null)
				c ();
		}

		public void OnError (FacebookException error)
		{
			var c = HandleError;
			if (c != null)
				c (error);
		}

		public void OnSuccess (Java.Lang.Object result)
		{
			var c = HandleSuccess;
			if (c != null)
				c (result.JavaCast<TResult> ());
		}
	}
}


using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook;

[assembly: MetaData ("com.facebook.sdk.ApplicationId", Value="@string/facebook_app_id")]
namespace FacebookService.Droid
{
	[Activity (Label = "Facebook Graph Explorer", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			FacebookSdk.SdkInitialize (this.ApplicationContext);

			ActionBar.SetIcon (null);

			LoadApplication (new App ());
		}
	}
}


using System;
using Xamarin.Facebook;

namespace Wiggin.Facebook.Droid
{
	public static class FBSdkHelper
	{
		public static AccessToken ToNative (this DroidAccessToken token) {
			Xamarin.Facebook.AccessTokenSource source;
			if (token.AccessTokenSource == AccessTokenSource.WEB_VIEW)
				source = Xamarin.Facebook.AccessTokenSource.WebView;
			else
				source = Xamarin.Facebook.AccessTokenSource.FacebookApplicationNative;
			
			var newToken = new AccessToken (
				token.Token,
				token.ApplicationId,
				token.UserId,
				token.Permissions,
				token.DeclinedPermissions,
				source,
				DateTimeHelper.ToUnixTime(token.ExpirationTime),
				DateTimeHelper.ToUnixTime(token.LastRefreshTime)
			);

			return newToken;
		}
	}
}


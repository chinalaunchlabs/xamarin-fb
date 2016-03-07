using System;
using Facebook.CoreKit;

namespace Wiggin.Facebook.iOS
{
	public static class FBSdkHelper
	{
		public static AccessToken ToNative(this iOSAccessToken token) {
			var permissions = new string[token.Permissions.Count];
			token.Permissions.CopyTo (permissions, 0);

			var declined = new string[token.DeclinedPermissions.Count];
			token.DeclinedPermissions.CopyTo (declined, 0);
				
			var newToken = new AccessToken (
				token.Token,
				permissions,
				declined,
				token.ApplicationId,
				token.UserId,
				DateTimeHelper.ToNSDate(token.ExpirationTime),
				DateTimeHelper.ToNSDate(token.LastRefreshTime)
           );

			return newToken;
		}
	}
}


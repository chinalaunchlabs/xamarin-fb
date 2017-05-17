using System;
using System.Collections.Generic;
using Facebook.CoreKit;

namespace Wiggin.Facebook.iOS
{
	public static class FBSdkHelper
	{
		public static AccessToken ToNative(this FbAccessToken token) {
			if (token == null)
				return null;

			string[] permissions = { };
			if (token.Permissions != null) {
				permissions = new string[token.Permissions.Count];
				token.Permissions.CopyTo(permissions, 0);
			}

			string[] declined = { };
			if (token.DeclinedPermissions != null) {
				declined = new string[token.DeclinedPermissions.Count];
				token.DeclinedPermissions.CopyTo(declined, 0);
			}

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

		public static FbAccessToken ToForms(this AccessToken token) {
			if (token == null)
				return null;

			var formsToken = new FbAccessToken();

			formsToken.Token = token.TokenString;
			formsToken.ApplicationId = token.AppID;
			formsToken.UserId = token.UserID;
			formsToken.Permissions = new List<string>();
			formsToken.DeclinedPermissions = new List<string>();
			formsToken.ExpirationTime = DateTimeHelper.FromNSDate(token.ExpirationDate);
			formsToken.LastRefreshTime = DateTimeHelper.FromNSDate (token.RefreshDate);
			formsToken.AccessTokenSource = AccessTokenSource.NONE;

			foreach (var p in token.Permissions) {
				formsToken.Permissions.Add (p.ToString ());
			}
			foreach (var p in token.DeclinedPermissions) {
				formsToken.DeclinedPermissions.Add (p.ToString ());
			}

			return formsToken;
		}
	}
}


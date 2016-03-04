using System;
using Wiggin.Facebook;
using System.Collections.Generic;
using Xamarin.Facebook;
using Wiggin.Facebook.Droid;

[assembly:Xamarin.Forms.Dependency(typeof(DroidAccessToken))]
namespace Wiggin.Facebook.Droid
{
	public class DroidAccessToken: IAccessToken
	{
		public string Token { get; set; }
		public string ApplicationId { get; set; }
		public string UserId { get; set; }
		public ICollection<string> Permissions { get; set; }
		public ICollection<string> DeclinedPermissions { get; set; }
		public AccessTokenSource AccessTokenSource { get; set; }
		public DateTime ExpirationTime { get; set; }
		public DateTime LastRefreshTime { get; set; }

		public bool IsExpired() {
			return ExpirationTime < DateTime.Now;
		}

		public DroidAccessToken(AccessToken token) {
			Token = token.Token;
			ApplicationId = token.ApplicationId;
			UserId = token.UserId;
			Permissions = token.Permissions;
			DeclinedPermissions = token.DeclinedPermissions;
			ExpirationTime = DateTimeHelper.FromUnixTime(token.Expires.Time);
			LastRefreshTime = DateTimeHelper.FromUnixTime (token.LastRefresh.Time);

			if (token.Source == Xamarin.Facebook.AccessTokenSource.WebView)
				AccessTokenSource = AccessTokenSource.WEB_VIEW;
			else
				AccessTokenSource = AccessTokenSource.FACEBOOK_APPLICATION;

		}
	}
}


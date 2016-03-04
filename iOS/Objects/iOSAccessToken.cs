using System;
using Wiggin.Facebook.iOS;
using Facebook.CoreKit;
using System.Collections.Generic;

[assembly:Xamarin.Forms.Dependency(typeof(iOSAccessToken))]
namespace Wiggin.Facebook.iOS
{
	public class iOSAccessToken: IAccessToken
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

		public iOSAccessToken(AccessToken token) {
			Token = token.TokenString;
			ApplicationId = token.AppID;
			UserId = token.UserID;
//			Permissions = new List<string>(token.Permissions.ToArray<string>());
//			DeclinedPermissions = new List<string>(token.DeclinedPermissions.ToArray<string>());
			ExpirationTime = DateTimeHelper.FromNSDate(token.ExpirationDate);
			LastRefreshTime = DateTimeHelper.FromNSDate (token.RefreshDate);
			AccessTokenSource = AccessTokenSource.NONE;

			foreach (var p in token.Permissions) {
				System.Diagnostics.Debug.WriteLine (p.ToString ());
			}
		}
	}
}


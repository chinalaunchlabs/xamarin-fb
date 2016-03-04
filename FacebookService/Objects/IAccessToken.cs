using System;
using System.Collections.Generic;

namespace Wiggin.Facebook
{
	public enum AccessTokenSource {
		FACEBOOK_APPLICATION,
		WEB_VIEW
	}

	public interface IAccessToken
	{
		// Properties
		string Token { get; set; }
		string ApplicationId { get; set; }
		string UserId { get; set; }
		ICollection<string> Permissions { get; set; }
		ICollection<string> DeclinedPermissions { get; set; }
		AccessTokenSource AccessTokenSource { get; set; }
		DateTime ExpirationTime { get; set; }
		DateTime LastRefreshTime { get; set; }

		// Methods
		bool IsExpired ();

	}
}


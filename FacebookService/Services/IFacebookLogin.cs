using System;
using System.Threading.Tasks;

namespace Wiggin.Facebook
{
	public interface IFacebookLogin
	{

		Task<IAccessToken> LogIn(string[] permissions);

		bool IsLoggedIn();

		IAccessToken GetAccessToken();

		// TODO: Remove this feature. Expect the user to just make a graph request for profile info.
		IProfile FetchProfile ();

	}
}


using System;
using System.Threading.Tasks;

namespace Wiggin.Facebook
{
	public interface IFacebookLogin
	{

		Task<IAccessToken> LogIn(string[] permissions);

		bool IsLoggedIn();

		IAccessToken GetAccessToken();

		IProfile FetchProfile ();

	}
}


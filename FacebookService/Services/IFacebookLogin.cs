using System;
using System.Threading.Tasks;

namespace Wiggin.Facebook
{
	public interface IFacebookLogin
	{

		Task<FbAccessToken> LogIn(string[] permissions);
		bool IsLoggedIn ();
		FbAccessToken GetAccessToken();
		void Logout();
	}
}


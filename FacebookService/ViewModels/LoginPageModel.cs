using System;
using FreshMvvm;
using PropertyChanged;
using System.Windows.Input;
using Xamarin.Forms;
using Wiggin.Facebook;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FacebookService
{
	[ImplementPropertyChanged]
	public class LoginPageModel: FreshBasePageModel
	{
		private string[] permissions = new string[] {
			"public_profile",
			"email"
		};

		private bool _profileNotLoaded = false;
		public bool ProfileNotLoaded {
			get { return _profileNotLoaded; }
			set { _profileNotLoaded = value; }
		}

		public ICommand LoginCommand {
			get {
				return new Command( async () => {
					_profileNotLoaded = true;
					IAccessToken token = await DependencyService.Get<IFacebookLogin>()
						.LogIn(permissions);
					IGraphRequest request = DependencyService.Get<IGraphRequest>()
						.NewRequest(token, "/me", "name, email, picture");
					IGraphResponse response = await request.ExecuteAsync();

					System.Diagnostics.Debug.WriteLine(response.RawResponse);

					Dictionary<string, string> deserialized = JsonConvert
						.DeserializeObject<Dictionary<string,string>>(response.RawResponse);
					FacebookProfile profile = new FacebookProfile(deserialized["name"], deserialized["email"], deserialized["id"]);

					await CoreMethods.PushPageModel<LoggedInPageModel>(profile);
				});
			}
		}
	}
}


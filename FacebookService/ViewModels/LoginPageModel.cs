using System;
using FreshMvvm;
using PropertyChanged;
using System.Windows.Input;
using Xamarin.Forms;
using Wiggin.Facebook;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

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
					IAccessToken token;
					try {
						token = await DependencyService.Get<IFacebookLogin>()
							.LogIn(permissions);

						ProfileNotLoaded = true;

						if (token != null) {
							IGraphRequest request = DependencyService.Get<IGraphRequest>()
								.NewRequest(token, "/me", "name, email");

							IGraphResponse response = await request.ExecuteAsync();
							System.Diagnostics.Debug.WriteLine(response.RawResponse);

							Dictionary<string, string> deserialized = JsonConvert
								.DeserializeObject<Dictionary<string,string>>(response.RawResponse);
							FacebookProfile profile = new FacebookProfile(deserialized["name"], deserialized["email"], deserialized["id"]);

							await CoreMethods.PushPageModel<LoggedInPageModel>(profile);

						}
						else {
							System.Diagnostics.Debug.WriteLine("token was null");
						}

					}
					catch (TaskCanceledException e) {
						System.Diagnostics.Debug.WriteLine("The task was canceledfsdmfdmsf.");
//						throw e;
					}


					ProfileNotLoaded = false;
				});
			}
		}
	}
}


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
			"email",
			"user_friends"
		};
		private Dictionary<string, string> parameters = new Dictionary<string, string> ();

		private bool _profileNotLoaded = false;
		public bool ProfileNotLoaded {
			get { return _profileNotLoaded; }
			set { _profileNotLoaded = value; }
		}

		public override void Init(object initData)
		{
			base.Init(initData);

			// try to check if token is available
			var token = FbAccessToken.Current;
			if (token != null) {
				System.Diagnostics.Debug.WriteLine("Token available");
				HandleAlreadyLoggedIn(token);
			}
		
		}

		async void HandleAlreadyLoggedIn(FbAccessToken token) {
			parameters.Add("fields", "name, email");
			IGraphRequest request = DependencyService.Get<IGraphRequest>()
					.NewRequest(token, "/me", parameters);

			IGraphResponse response = await request.ExecuteAsync();
			System.Diagnostics.Debug.WriteLine(response.RawResponse);

			Dictionary<string, string> deserialized = JsonConvert
				.DeserializeObject<Dictionary<string, string>>(response.RawResponse);
			FacebookProfile profile = new FacebookProfile(deserialized["name"], deserialized["email"], deserialized["id"]);

			await CoreMethods.PushPageModel<LoggedInPageModel>(profile);
		}

		public ICommand LoginCommand {
			get {
				return new Command( async (dsf) => {
					FbAccessToken token;
					try {
						token = await DependencyService.Get<IFacebookLogin>()
							.LogIn(permissions);

						ProfileNotLoaded = true;

						if (token != null) {
							// save token
							token.Save();

							parameters.Add("fields", "name, email");
							IGraphRequest request = DependencyService.Get<IGraphRequest>()
								.NewRequest(token, "/me", parameters);

							IGraphResponse response = await request.ExecuteAsync();
							System.Diagnostics.Debug.WriteLine(response.RawResponse);

							Dictionary<string, string> deserialized = JsonConvert
								.DeserializeObject<Dictionary<string,string>>(response.RawResponse);
							FacebookProfile profile = new FacebookProfile(deserialized["name"], deserialized["email"], deserialized["id"]);

							await CoreMethods.PopToRoot(true);
							await CoreMethods.PushPageModel<LoggedInPageModel>(profile);

						}
						else {
							System.Diagnostics.Debug.WriteLine("token was null");
						}

					}
					catch (TaskCanceledException e) {
						System.Diagnostics.Debug.WriteLine("The task was canceled.");
//						throw e;
					}


					ProfileNotLoaded = false;
				});
			}
		}
	}
}


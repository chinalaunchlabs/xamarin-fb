using System;

using Xamarin.Forms;
using Wiggin.Facebook;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FacebookService
{
	public class App : Application
	{
		public App ()
		{
			var fbLoginButton = new Button {
				Text = "Log in with Facebook"
			};

			var nameLabel = new Label {
				Text = "Name: "
			};

			var appLabel = new Label {
				Text = "App ID: "
			};

			var emailLabel = new Label {
				Text = "Email: "
			};

			fbLoginButton.Clicked += async (object sender, EventArgs e) => {
				IAccessToken token = await DependencyService.Get<IFacebookLogin>().LogIn(new string[] {"public_profile", "email"});
				IProfile profile = DependencyService.Get<IFacebookLogin>().FetchProfile();
				nameLabel.Text += profile.Name;
				appLabel.Text += token.ApplicationId;
				fbLoginButton.IsVisible = false;

				IGraphRequest req = DependencyService.Get<IGraphRequest>().NewRequest(token, "/me");
				req.SetParams("name, email");

				IGraphResponse response = await req.ExecuteAsync();
				System.Diagnostics.Debug.WriteLine("Response: " + response.RawResponse);
				Dictionary<string, string> serialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.RawResponse);
				System.Diagnostics.Debug.WriteLine(serialized["email"]);
				emailLabel.Text += serialized["email"];
			};

			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						nameLabel,
						appLabel,
						emailLabel,
						fbLoginButton
					}
				}
			};
		}
	}
}


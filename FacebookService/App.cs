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
			var loadingIndicator = new ActivityIndicator ();
			loadingIndicator.IsEnabled = false;

			fbLoginButton.Clicked += async (object sender, EventArgs e) => {
				loadingIndicator.IsEnabled = true;
				loadingIndicator.IsRunning = true;
				loadingIndicator.IsVisible = true;
				IAccessToken token = await DependencyService.Get<IFacebookLogin>().LogIn(new string[] {"public_profile", "email"});
				System.Diagnostics.Debug.WriteLine(token.Token);
				fbLoginButton.IsVisible = false;

				IGraphRequest req = DependencyService.Get<IGraphRequest>().NewRequest(token, "/me", "name, email");

				IGraphResponse response = await req.ExecuteAsync();
				System.Diagnostics.Debug.WriteLine("Response: " + response.RawResponse);
				Dictionary<string, string> serialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.RawResponse);

				emailLabel.Text += serialized["email"];
				nameLabel.Text += serialized["name"];
				appLabel.Text += serialized["id"];

				loadingIndicator.IsEnabled = false;
				loadingIndicator.IsRunning = false;
				loadingIndicator.IsVisible = false;

			};

			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						nameLabel,
						appLabel,
						emailLabel,
						fbLoginButton,
						loadingIndicator,
					}
				}
			};
		}
	}
}


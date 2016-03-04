using System;

using Xamarin.Forms;
using Wiggin.Facebook;

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

			fbLoginButton.Clicked += async (object sender, EventArgs e) => {
				IAccessToken token = await DependencyService.Get<IFacebookLogin>().LogIn(new string[] {"public_profile", "email"});
				IProfile profile = DependencyService.Get<IFacebookLogin>().FetchProfile();
				nameLabel.Text += profile.Name;
				appLabel.Text += token.ApplicationId;
				fbLoginButton.IsVisible = false;
			};

			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						nameLabel,
						appLabel,
						fbLoginButton
					}
				}
			};
		}
	}
}


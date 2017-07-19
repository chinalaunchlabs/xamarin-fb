using System;
using FreshMvvm;
using PropertyChanged;
using System.Windows.Input;
using Xamarin.Forms;
using Wiggin.Facebook;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FacebookService
{
	[ImplementPropertyChanged]
	public class LoggedInPageModel: FreshBasePageModel
	{
		private FacebookProfile _profile;
		public LoggedInPageModel ()
		{
			LoadedFriends = false;
		}

		public override void Init (object initData)
		{
			base.Init (initData);
			_profile = (FacebookProfile)initData;
			LoadPic ();
		}

		private async void LoadPic() {

			var token = FbAccessToken.Current;
			if (token == null)
				return;
			
			var parameters = new Dictionary<string, string>();
			parameters.Add("redirect", "false");

			var request = DependencyService.Get<IGraphRequest> ().NewRequest (token, "/me/picture", parameters);
			var response = await request.ExecuteAsync();
			System.Diagnostics.Debug.WriteLine(response.RawResponse);

			JObject fbPicture = JObject.Parse(response.RawResponse);
			Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>> (fbPicture ["data"].ToString());
			ProfilePicture = data ["url"];
		}

		public string WelcomeText {
			get { return "Welcome " + _profile.Name + "!"; }
		}

		public string Email {
			get { return "Your email is " + _profile.Email + " ."; }
		}

		public string AppId {
			get { return _profile.AppId; }
		}

		private string _profilePicture;
		public string ProfilePicture {
			get { return _profilePicture; }
			set { _profilePicture = value; }
		}

		private List<FacebookFriend> _friends = new List<FacebookFriend>();
		public List<FacebookFriend> Friends {
			get {
				return _friends;
			}
			set {
				_friends = value;
			}
		}

		public bool LoadedFriends { get; set; }

		public ICommand LoadFriendsCommand {
			get {
				return new Command (async (obj) => {
					var token = FbAccessToken.Current;
					if (token == null)
						return;

					var request = DependencyService.Get<IGraphRequest> ().NewRequest (token, "/me/friends", null);
					var response = await request.ExecuteAsync();
					System.Diagnostics.Debug.WriteLine(response.RawResponse);

					JObject fbFriends = JObject.Parse(response.RawResponse);
					IList<JToken> friends = (IList<JToken>)fbFriends["data"];
					var tmp = new List<FacebookFriend>();
					foreach (var friend in friends) {
						tmp.Add(JsonConvert.DeserializeObject<FacebookFriend>(friend.ToString()));
					}
					Friends = tmp;
					LoadedFriends = true;
					System.Diagnostics.Debug.WriteLine("Friends loaded...");
				});
			}
		}

		public ICommand LogoutCommand {
			get {
				return new Command ( async () => {
					DependencyService.Get<IFacebookLogin>().Logout();
					System.Diagnostics.Debug.WriteLine("Am I still logged in? " + DependencyService.Get<IFacebookLogin>().IsLoggedIn());
				
					await CoreMethods.PopToRoot(true);
					await CoreMethods.PushPageModel<LoginPageModel>();
				});
			}
		}
	}
}


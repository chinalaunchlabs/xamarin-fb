using System;
using FreshMvvm;
using PropertyChanged;

namespace FacebookService
{
	[ImplementPropertyChanged]
	public class LoggedInPageModel: FreshBasePageModel
	{
		private FacebookProfile _profile;
		public LoggedInPageModel ()
		{
		}

		public override void Init (object initData)
		{
			base.Init (initData);
			_profile = (FacebookProfile)initData;
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
	}
}


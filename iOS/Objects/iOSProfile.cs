using System;
using Wiggin.Facebook.iOS;
using Facebook.CoreKit;

[assembly:Xamarin.Forms.Dependency(typeof(iOSProfile))]
namespace Wiggin.Facebook.iOS
{
	public class iOSProfile: IProfile
	{

		public string Id { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Name { get; set; }
		public Uri LinkUri { get; set; }

		public iOSProfile (Profile profile)
		{
			Id = profile.UserID;
			FirstName = profile.FirstName;
			MiddleName = profile.MiddleName;
			LastName = profile.LastName;
			Name = profile.Name;
			LinkUri = new Uri (profile.LinkUrl.ToString ());
		}
	}
}


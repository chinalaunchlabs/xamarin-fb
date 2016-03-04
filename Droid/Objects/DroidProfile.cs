using System;
using Wiggin.Facebook;
using Wiggin.Facebook.Droid;
using Xamarin.Facebook;

[assembly:Xamarin.Forms.Dependency(typeof(DroidProfile))]
namespace Wiggin.Facebook.Droid
{
	public class DroidProfile: IProfile
	{

		public string Id { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Name { get; set; }
		public Uri LinkUri { get; set; }

		public DroidProfile (Profile profile)
		{
			Id = profile.Id;
			FirstName = profile.FirstName;
			MiddleName = profile.MiddleName;
			LastName = profile.LastName;
			Name = profile.Name;
			LinkUri = new Uri(profile.LinkUri.ToString());
		}
	}
}


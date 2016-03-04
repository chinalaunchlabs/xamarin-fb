using System;

namespace Wiggin.Facebook
{
	public interface IProfile
	{
		string Id { get; set; }
		string FirstName { get; set; }
		string MiddleName { get; set; }
		string LastName { get; set; }
		string Name { get; set; }
		Uri LinkUri { get; set; }
	}
}


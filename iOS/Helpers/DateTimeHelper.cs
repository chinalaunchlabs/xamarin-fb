using System;
using Foundation;

namespace Wiggin.Facebook.iOS
{
	// TODO: Check accuracy of conversion.
	public static class DateTimeHelper
	{
		public static DateTime FromNSDate(NSDate date) {
			DateTime reference = new DateTime(2001, 1, 1, 0, 0, 0);
			DateTime currentDate = reference.AddSeconds(date.SecondsSinceReferenceDate);
			DateTime localDate = currentDate.ToLocalTime ();
			return localDate;
		}

		public static NSDate ToNSDate(DateTime date) {
			DateTime newDate = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0) );
			return NSDate.FromTimeIntervalSinceReferenceDate((date - newDate).TotalSeconds);
		}
	}
}


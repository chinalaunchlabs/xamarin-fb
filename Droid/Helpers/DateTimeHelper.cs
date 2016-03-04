using System;

namespace Wiggin.Facebook.Droid
{
	public static class DateTimeHelper
	{
		public static DateTime FromUnixTime(long timeInMillis) {
			var epoch = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return epoch.AddMilliseconds (timeInMillis);
		}
	}
}


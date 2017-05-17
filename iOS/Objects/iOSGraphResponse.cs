using System;
using Wiggin.Facebook.iOS;
using Foundation;

[assembly:Xamarin.Forms.Dependency(typeof(iOSGraphResponse))]
namespace Wiggin.Facebook.iOS
{
	public class iOSGraphResponse: IGraphResponse
	{
		public string RawResponse { get; set; }

		public iOSGraphResponse (NSDictionary graphResponse)
		{
			NSError error;
			NSData jsonData = NSJsonSerialization.Serialize (graphResponse, NSJsonWritingOptions.PrettyPrinted, out error);

			RawResponse = jsonData.ToString();
		
		}
	}
}


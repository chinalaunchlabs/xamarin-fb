using System;
using Wiggin.Facebook.Droid;
using Xamarin.Facebook;

[assembly:Xamarin.Forms.Dependency(typeof(DroidGraphResponse))]
namespace Wiggin.Facebook.Droid
{
	public class DroidGraphResponse: IGraphResponse
	{
		public string RawResponse { get ; set; }

		public DroidGraphResponse (GraphResponse graphResponse)
		{
			RawResponse = graphResponse.RawResponse;
		}
	}
}


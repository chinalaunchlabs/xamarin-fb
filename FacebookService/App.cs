using System;

using Xamarin.Forms;
using Wiggin.Facebook;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreshMvvm;

namespace FacebookService
{
	public class App : Application
	{
		public App ()
		{
			var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel> ();
			var basicNavContainer = new FreshNavigationContainer (page);

			MainPage = basicNavContainer;
		}
	}
}


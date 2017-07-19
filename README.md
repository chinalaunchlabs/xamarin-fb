The Facebook SDK implemented as a dependency service for PCL Xamarin projects.

## Testing
*If you just want to see the basic functionality and don't want to change the app ids, etc.*

**Test User:** john_maykkgj_doe@tfbnw.net

**Password:** test1234

## Integrating with Your App
Please check the Getting Started documentation of the Xamarin Facebook SDK for [iOS](https://components.xamarin.com/gettingstarted/facebookios) and [Android](https://components.xamarin.com/gettingstarted/facebookandroid).

Relevant files are found in:
```
FacebookService/
	Objects/
	Services/
FacebookService.Droid/
	Helpers/
	Objects/
	Services/
	FacebookLoginActivity.cs
FacebookService.iOS/
	Helpers/
	Objects/
	Services/
```

## Usage
### Logging In
```
var permissions = new string[] { "public_profile", "email" };
FbAccessToken token;
try {
	token = await DependencyService.Get<IFacebookLogin>().LogIn(permissions);
	System.Diagnostics.Debug.WriteLine("Login successful!");
}
catch (TaskCanceledException e) {
	System.Diagnostics.Debug.WriteLine("Login was canceled");
}
```

### Fetching Data
```
var parameters = new Dictionary<string,string>();
parameters.Add("fields", "name, email");

IGraphRequest request = DependencyService.Get<IGraphRequest>().NewRequest(token, "/me", parameters);
IGraphResponse response = await request.ExecuteAsync();
Dictionary<string, string> deserialized = JsonConvert.DeserializeObject<Dictionary<string,string>>(response.RawResponse);
System.Diagnostics.Debug.WriteLine("Hello {0}!", deserialized["name"]);
```

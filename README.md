## Overview
The Facebook SDK implemented as a dependency service for PCL Xamarin projects.

## Testing
*If you just want to see the basic functionality and don't want to change the app ids, etc.*
**Test User:** ccizldw_riceberg_1457337743@tfbnw.net

**Password:** test1234

## To Integrate with Your Own App
Please check the Getting Started documentation of the Xamarin Facebook SDK for [iOS](https://components.xamarin.com/gettingstarted/facebookios) and [Android](https://components.xamarin.com/gettingstarted/facebookandroid).

## Usage
### Logging In
```
var permissions = new string[] { "public_profile", "email" };
IAccessToken token;
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
Dictionary<string, string> deserialized = JsonConvert.DeserializeObject<Dictionary<string,string>>(response.RawResponse);
System.Diagnostics.Debug.WriteLine("Hello {0}!", deserialized["name"]);
```
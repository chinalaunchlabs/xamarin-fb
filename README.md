## Overview
The Facebook SDK implemented as a dependency service for PCL Xamarin projects.

## Testing

**Test User:** ccizldw_riceberg_1457337743@tfbnw.net
**Password:** test1234

## Usage
### Logging In
```
var permissions = new string[] { "public_profile", "email"};
IAccessToken token;
try {
	token = await DependencyService.Get<IFacebookLogin>().LogIn(permissions);
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
```
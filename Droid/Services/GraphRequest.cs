﻿using System;
using Xamarin.Facebook;
using Wiggin.Facebook.Droid;
using System.Threading.Tasks;
using Android.OS;
using System.Collections.Generic;
using Xamarin.Facebook.Share.Model;

[assembly:Xamarin.Forms.Dependency(typeof(GraphRequestService))]
namespace Wiggin.Facebook.Droid
{
	public class GraphRequestService: IGraphRequest
	{
		public string Path { get; set; }
		public string HttpMethod { get; set; }
		public string Version { get; set; }
		public IAccessToken AccessToken {
			get {
				return new DroidAccessToken (_token);
			}
		}

		private AccessToken _token;
		private GraphRequest _request;


		public IGraphRequest NewRequest(IAccessToken token, string path, Dictionary<string,string> parameters, string httpMethod = default(string), string version = default(string)) {

			Initialize (token, path, httpMethod, version);

			if (parameters != null) {
				var bundle = new Bundle ();

				foreach (var key in parameters.Keys) {
					System.Diagnostics.Debug.WriteLine (key + " : " + parameters [key]);
					bundle.PutString (key, parameters [key]);
				}

				_request.Parameters = bundle;
			}

			return this;
		}

//		public IGraphRequest NewRequest(IAccessToken token, string path, string parameters, string httpMethod = default(string), string version = default(string)) {
//
//			Initialize (token, path, httpMethod, version);
//
//			var bundle = new Bundle();
//			bundle.PutString("fields", parameters);
//			_request.Parameters = bundle;
//
//			return this;
//		}

		public Task<IGraphResponse> ExecuteAsync() {
			TaskCompletionSource<IGraphResponse> tcs = new TaskCompletionSource<IGraphResponse> ();

			((GraphCallback)_request.Callback).RequestCompleted += (object sender, GraphResponseEventArgs e) => {
				DroidGraphResponse resp = new DroidGraphResponse(e.Response);
				tcs.SetResult(resp);
			};

			_request.ExecuteAsync ();
		
			return tcs.Task;
		}

		private void Initialize(IAccessToken token, string path, string httpMethod = default(string), string version = default(string)) {
			_token = (token as DroidAccessToken).ToNative ();
			Path = path;
			HttpMethod = httpMethod;
			Version = version;

			GraphCallback callback = new GraphCallback ();
			_request = new GraphRequest (_token, Path, null, null, callback);
		}

	}
}


using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Wiggin.Facebook
{
	public interface IGraphRequest
	{
		string Path { get; set; }
		string HttpMethod { get; set; }
		string Version { get; set; }
		IAccessToken AccessToken { get; }

		IGraphRequest NewRequest (IAccessToken token, string path, string parameters, string httpMethod = default(string), string version = default(string));
		Task<IGraphResponse> ExecuteAsync();
//		void SetParams (string parameters);
				
	}
}


using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Web;

namespace Notifications.Ui.Web.ApplicationServices.Authentication
{
	internal class AuthenticationProvider : IAuthenticationProvider
	{
		public static readonly string[] Scopes = {"email", "Directory.Read.All", "User.Read"};
		private readonly ITokenAcquisition _tokenAcquisition;

		public AuthenticationProvider(ITokenAcquisition tokenAcquisition)
		{
			_tokenAcquisition = tokenAcquisition;
		}

		public async Task AuthenticateRequestAsync(HttpRequestMessage request)
		{
			var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(Scopes);

			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
		}
	}
}
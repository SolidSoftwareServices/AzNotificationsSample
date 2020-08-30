using System.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace Notifications.Ui.Web.ApplicationServices.Authentication
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddAppAuthentication(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddMicrosoftIdentityWebAppAuthentication(configuration)
				.EnableTokenAcquisitionToCallDownstreamApi(AuthenticationProvider.Scopes)
				.AddDistributedTokenCaches();

			services.AddControllersWithViews(options =>
			{
				var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();
				options.Filters.Add(new AuthorizeFilter(policy));
			}).AddMicrosoftIdentityUI();

			services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();
			return services;
		}
	}
}

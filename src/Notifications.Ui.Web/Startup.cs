using System;
using System.Linq;
using Core.Cqrs;
using Core.Messaging.Azure.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Ui.DataServices.Azure.Infrastructure;
using Notifications.Ui.DomainServices.Infrastructure;
using Notifications.Ui.Events.Hubs;
using Notifications.Ui.Events.Infrastructure;
using Notifications.Ui.Web.ApplicationServices;
using Notifications.Ui.Web.ApplicationServices.Authentication;
using Notifications.Ui.Web.ApplicationServices.Settings;

namespace Notifications.Ui.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			AppSettings=new Lazy<AppSettings>(()=>new AppSettings(configuration));
		}

		public IConfiguration Configuration { get; }
		private Lazy<AppSettings> AppSettings { get; }
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCosmosCache(cacheOptions =>
			{
				cacheOptions.ContainerName = Configuration["CosmosCache:ContainerName"];
				cacheOptions.DatabaseName = Configuration["CosmosCache:DatabaseName"];
				cacheOptions.ClientBuilder = new CosmosClientBuilder(Configuration["CosmosCache:ConnectionString"]);
				cacheOptions.CreateIfNotExists = true;
			});

			services.AddHttpClient();

			services.AddRazorPages()
				.AddRazorRuntimeCompilation();
			services.AddServerSideBlazor()
				.AddCircuitOptions(o => { o.DetailedErrors = true; });


			services.AddHttpContextAccessor();

			services.AddResponseCompression(opts =>
			{
				opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {"application/octet-stream"});
			});
			services
				.AddAppSettings()
				
				//-aspects
				.AddAppAuthentication(Configuration)
				
				//comm-ports
				.AddUiEventHubs()
				.AddAzureServiceBusAsCommandDispatcher()
				.AddInProcQueryResolver()

				//layers
				.AddAzureNotificationDataServices()
				.AddDomainServices();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//if (env.IsDevelopment())
			//{
			//	app.UseDeveloperExceptionPage();
			//}
			//else
			//{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
			//}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			
			//app.UseSession();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapBlazorHub();
				
				endpoints.MapHub<NotificationEventsHub>(AppSettings.Value.NotificationEventsUrl);

				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}
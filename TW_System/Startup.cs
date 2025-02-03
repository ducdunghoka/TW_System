using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using TW.Common;
using TW_System.Common;
using TW_System.Models;

namespace TW_System
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
			if (string.IsNullOrEmpty(connectionString))
			{
				throw new Exception("DefaultConnection is missing or invalid in appsettings.json");
			}

			// Chỉ đăng ký TWContext và bỏ localization
			services.AddDbContext<TWContext>(options =>
				options.UseSqlServer(connectionString));

			// Bỏ phần localization
			// services.AddSqlLocalization(options =>
			// {
			//     options.CreateNewRecordWhenLocalisedStringDoesNotExist = true;
			//     options.UseTypeFullNames = true;
			// });

			var authFolder = Configuration["Authentication"].ToString();
			services.AddDataProtection().PersistKeysToFileSystem(new System.IO.DirectoryInfo(authFolder)).SetApplicationName("NETCORE");

			services.AddAuthentication(AuthenticationTypes.ApplicationCookie).AddCookie(AuthenticationTypes.ApplicationCookie, options =>
			{
				options.Cookie = new CookieBuilder
				{
					HttpOnly = true,
					Name = ".ROSAuth.Cookie",
					Path = "/",
					IsEssential = true
				};

				options.LoginPath = "/login";
				options.ExpireTimeSpan = TimeSpan.FromDays(7);
				options.SlidingExpiration = true;
				options.ReturnUrlParameter = "path";
			});

			services.AddSignalR();
			services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizePolicyProvider>();
			services.AddScoped<IAuthorizationHandler, CustomAuthorizeHandler>();
			services.AddScoped<IMySessions, MySessions>();

			services.ConfigureApplicationCookie(options =>
			{
				options.AccessDeniedPath = new PathString("/logout");
			});

			services.AddControllersWithViews(options =>
			{
				//options.Filters.Add(new Areas.PAYSLIP.Helper.MasterAttribute());
			}).AddRazorRuntimeCompilation().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

			services.AddSession(options =>
			{
				options.Cookie.Name = ".ROSAuth.Session";
				options.IdleTimeout = TimeSpan.FromHours(1);
				options.Cookie.IsEssential = true;
			});

			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddCors();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			var viCultureInfo = new CultureInfo("vi");
			viCultureInfo.NumberFormat.CurrencyDecimalDigits = 2;
			viCultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
			viCultureInfo.NumberFormat.NumberDecimalSeparator = ".";
			viCultureInfo.NumberFormat.CurrencyGroupSeparator = ",";

			var zhCultureInfo = new CultureInfo("zh");
			zhCultureInfo.NumberFormat.CurrencyDecimalDigits = 2;
			zhCultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
			zhCultureInfo.NumberFormat.NumberDecimalSeparator = ".";
			zhCultureInfo.NumberFormat.CurrencyGroupSeparator = ",";

			app.UseDeveloperExceptionPage();

			var supportedCultures = new[] { viCultureInfo, zhCultureInfo };

			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("vi"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures,
			});

			app.UseStaticFiles();
			app.UseSession();
			app.UseRouting();
			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<ServerHub>("/serverhub");
				endpoints.MapHub<LuckyDrawHub>("/luckydrawhub");
				endpoints.MapControllerRoute(
					name: "areas",
					pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}

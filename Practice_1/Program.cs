using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Practice_1.Admin.Models;

namespace Practice_1
{
    public class Program
    {
        public static void Main()
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				 .AddCookie(options =>
				 {
					 options.LoginPath = "/home/login";
					 options.AccessDeniedPath = "/home/accessdenied";
				 });
			//         builder.Services.AddAuthorization(x =>
			//{
			//	x.AddPolicy("AdminArea", policy => { policy.RequireRole("Admin"); });
			//});
			builder.Services.AddAuthorization(opts => {

				opts.AddPolicy("AdminArea", policy => {
					policy.RequireClaim("Role", "Admin");
				});
			});

			string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            builder.Services.AddMvc();

            var app = builder.Build();

            app.UseAuthentication();

            app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
			});

			app.Run();
        }
    }
}
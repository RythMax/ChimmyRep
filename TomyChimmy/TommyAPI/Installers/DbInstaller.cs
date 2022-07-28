using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI;
using TommyAPI.Data;
using TommyAPI.Models;

namespace TommyAPI.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<TommyAPIContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TomyChimmyConnection")));

            services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole>()
            .AddDefaultUI()
            .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<TommyAPIContext>()
            .AddDefaultTokenProviders();

            /*services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TommyAPIContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<IdentityRole>>();*/


            //services.AddScoped<DbContext, TommyAPIContext>();
            
            services.AddControllers();
        }
    }
}

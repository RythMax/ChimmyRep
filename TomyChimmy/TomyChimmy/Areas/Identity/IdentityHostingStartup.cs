using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TomyChimmy.Areas.Identity.Data;
using TomyChimmy.Data;

[assembly: HostingStartup(typeof(TomyChimmy.Areas.Identity.IdentityHostingStartup))]
namespace TomyChimmy.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TomyChimmyDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TomyChimmyDbContextConnection")));

                services.AddDefaultIdentity<User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<TomyChimmyDbContext>()
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();
                //serias dudas sobre User Claims principal Factory<User> por que en el tutorial decia ApplicationUserClaimsPrincipalFactory
                //services.AddScoped<IUserClaimsPrincipalFactory<User>, ApplicationUserClaimsPrincipalFactory>();
                    //.AddEntityFrameworkStores<TomyChimmyDbContext>();
            });
        }
    }
}
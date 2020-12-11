using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicCollaboration.Areas.Identity.Data;
using MusicCollaboration.Data;

[assembly: HostingStartup(typeof(MusicCollaboration.Areas.Identity.IdentityHostingStartup))]
namespace MusicCollaboration.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MusicCollaborationContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MusicCollaborationContextConnection")));

                services.AddDefaultIdentity<MusicCollaborationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<MusicCollaborationContext>();
            });
        }
    }
}
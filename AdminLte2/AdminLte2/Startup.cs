using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(AdminLte2.Startup))]
namespace AdminLte2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Autenticacao/Login"),
                ExpireTimeSpan = TimeSpan.FromHours(1)

            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login";
        }
    }
}
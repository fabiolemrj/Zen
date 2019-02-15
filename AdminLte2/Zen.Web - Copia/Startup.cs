using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(Zen.Web.Startup))]
namespace Zen.Web
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
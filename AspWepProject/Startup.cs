using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Owin;
using AspWepProject.Models;

[assembly: OwinStartupAttribute(typeof(AspWepProject.Startup))]
namespace AspWepProject
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "783628847902-fsl1fpvl94lsni7ovhc5d30ofeikqr3l.apps.googleusercontent.com",
                ClientSecret = "y1fsv8Wm1kbxaDTbbNNgjAAY"
            });
        }
    }
}

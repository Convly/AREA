using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(WebClient.AuthConfig))]
namespace WebClient
{
    /// <summary>
    /// Handles the authentification with cookies
    /// </summary>
    public class AuthConfig
    {
        /// <summary>
        /// Add a cookie when the user is connected
        /// </summary>
        /// <param name="app">The main application</param>
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Auth/Login")
            });
        }
    }
}
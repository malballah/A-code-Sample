using System;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Microsoft.Practices.Unity.WebApi;
using Store.Web.App_Start;


[assembly: OwinStartupAttribute(typeof(Store.Startup))]
namespace Store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

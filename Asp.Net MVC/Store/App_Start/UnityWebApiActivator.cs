using System.Web.Http;
using Microsoft.Practices.Unity.WebApi;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TourGuide.Web.App_Start.UnityWebApiActivator), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(TourGuide.Web.App_Start.UnityWebApiActivator), "Shutdown")]

namespace Store.Web.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET</summary>
    public static class UnityWebApiActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start(HttpConfiguration config) 
        {
            // Use UnityHierarchicalDependencyResolver if you want to use a new child container for each IHttpController resolution.
             //var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.GetConfiguredContainer());
             var resolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());

            config.DependencyResolver = resolver;
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}

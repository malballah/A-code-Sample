using System;
using System.Data.Entity;
using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using Truck.Controllers;
using Truck.Data;
using Truck.Data.Infrastructure;
using Truck.Data.Repositories;
using Truck.Infrastructure;
using Truck.Infrastructure.Services;
using Truck.Models;
using Truck.ViewModels;
using Truck.Services;


namespace Truck
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ApplicationDbContext, ApplicationDbContext>(new PerRequestLifetimeManager());
            container.RegisterType<IMailService,MailService>(new PerRequestLifetimeManager());
            container.RegisterType<DbContext,TruckDbContext>(new PerRequestLifetimeManager());
            container.RegisterType<IDbFactory, DbFactory>(new PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType(typeof(IRepository<>),typeof(Repository<>));
            container.RegisterType(typeof(IDbService<>), typeof(DbService<>));
            container.RegisterType<IAssignmentService, AssignmentService>(new PerRequestLifetimeManager());
            container.RegisterType<IUploadFileService, UploadFileService>(new PerRequestLifetimeManager());
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(
                new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new HierarchicalLifetimeManager());

            container.RegisterType<AccountController>(
                new InjectionConstructor());
            var imapper = AutoMapperConfiguration.Configure();
            container.RegisterInstance<IMapper>(imapper);
        }
    }
}

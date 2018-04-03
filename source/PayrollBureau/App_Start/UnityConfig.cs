using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using PayrollBureau.Business.Helper;
using PayrollBureau.Data.Interfaces;
using PayrollBureau.Data.Models;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace PayrollBureau
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            container.RegisterType<HttpContextBase>(new PerRequestLifetimeManager(), new InjectionFactory(_ => new HttpContextWrapper(HttpContext.Current)));
            container.RegisterType<HttpRequestBase>(new PerRequestLifetimeManager(), new InjectionFactory(_ => new HttpRequestWrapper(HttpContext.Current.Request)));
            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IDatabaseFactory<PayrollBureauDatabase>, PayrollBureauDatabaseFactory>(new InjectionConstructor(
                    new InjectionParameter<string>(ConfigHelper.DefaultConnection)
                 ));

            // Register everything in these namespaces based on convention:
            var conventionBasedMappings = new[]
            {
                "PayrollBureau.Data.Services",
                "PayrollBureau.Data.Interfaces",
                "PayrollBureau.Business.Services",
                "PayrollBureau.Business.Interfaces"
            };

            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies().Where(tt => conventionBasedMappings.Any(n => n == tt.Namespace)),
                WithMappings.FromMatchingInterface,
                WithName.Default
             );

        }
    }
}
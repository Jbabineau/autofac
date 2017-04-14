using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Homesite.Services.AddressBroker.Services.Contracts;
using Homesite.Services.AddressBroker.Services.Implementations;
using System.Reflection;
using Homesite.Services.AddressBroker.DAL.Implementations;
using Homesite.Services.AddressBroker.Repository;
using Centrus.AddressBroker;

namespace Homesite.Services.AddressBroker.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            
            
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ZipcodeService>().AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().AsImplementedInterfaces();
            builder.RegisterType<LookupTypeRepository>().AsImplementedInterfaces();
            builder.RegisterType<MapFormatRepository>().AsImplementedInterfaces();
            builder.RegisterType<MapOutputFieldsRepository>().AsImplementedInterfaces();
            builder.RegisterType<MapRepository>().AsImplementedInterfaces();
            builder.RegisterType<StateMapRepository>().AsImplementedInterfaces();
            builder.RegisterType<StateRepository>().AsImplementedInterfaces();
            builder.RegisterType<OutputRepository>().AsImplementedInterfaces();
            builder.RegisterType<ABClientService>().AsImplementedInterfaces();
            builder.RegisterType<AddressBrokerConfigurationService>().AsImplementedInterfaces();
            builder.RegisterType<ABClient>().AsSelf();

            //builder.RegisterAssemblyTypes(
            //    Assembly.GetExecutingAssembly())
            //        .Where(t =>
            //        !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t));

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver; 
        }
    }
}

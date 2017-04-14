using Homesite.Services.AddressBroker.DAL.Contracts;
using Homesite.Services.AddressBroker.Repository;
using Homesite.Services.AddressBroker.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Homesite.Services.AddressBroker.DAL.Implementations;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DatabaseFactory>().AsImplementedInterfaces();
            builder.RegisterType<UnitOfWork>().AsImplementedInterfaces();
            builder.RegisterType<LookupTypeRepository>().AsImplementedInterfaces();
            builder.RegisterType<MapFormatRepository>().AsImplementedInterfaces();
            builder.RegisterType<MapOutputFieldsRepository>().AsImplementedInterfaces();
            builder.RegisterType<MapRepository>().AsImplementedInterfaces();
            builder.RegisterType<StateMapRepository>().AsImplementedInterfaces();
            builder.RegisterType<StateRepository>().AsImplementedInterfaces();
            builder.RegisterType<AddressBrokerConfigurationService>().AsSelf();
            builder.RegisterType<OutputRepository>().AsImplementedInterfaces();

            var container = builder.Build();

            using(var scope = container.BeginLifetimeScope())
            {
                var abconfig = scope.Resolve<AddressBrokerConfigurationService>();
                var config = abconfig.GetConfiguration();
                var maConfig = abconfig.GetConfigurationByState("ma");
                
            }
        }
    }
}

using Autofac;
using Microsoft.Extensions.Logging;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.Readers;
using TaxpayerAlerter.DAL.Readers.Base;
using TaxpayerAlerter.DAL.RestServices;
using TaxpayerAlerter.DAL.RestServices.Base;
using TaxpayerAlerter.DAL.Writers;
using TaxpayerAlerter.DAL.Writers.Base;

namespace TaxpayerAlerter.DAL.Registrators
{
    public static class Configuration
    {
        public static ContainerBuilder RegisterDAL(this ContainerBuilder builder)
        {
            builder.RegisterType<XLSXReader>().As<IReader<ClientDAO>>();
            builder.RegisterType<DOCXWriter>().As<IWriter<ClientDAO>>();
            builder.RegisterType<XLSXWriter>().As<IWriter<ClientDAO>>();

            builder.Register(c => new HttpClient()).SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            builder.RegisterType<ClientRestService>().As<IRestService<ClientDAO>>();

            return builder;
        }
    }
}
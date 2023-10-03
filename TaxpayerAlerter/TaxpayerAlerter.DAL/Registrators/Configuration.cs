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

            builder.RegisterType<ClientRestService>().As<IRestService<ClientDAO>>();

            builder.RegisterType<XLSXReader>().Keyed<IReader<ClientDAO>>("XLSXReader");
            builder.RegisterType<DOCXWriter>().Keyed<IWriter<ClientDAO>>("DOCXWriter");
            builder.RegisterType<XLSXWriter>().Keyed<IWriter<ClientDAO>>("XLSXWriter");

            builder.Register(c => new HttpClient()).SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            return builder;
        }
    }
}
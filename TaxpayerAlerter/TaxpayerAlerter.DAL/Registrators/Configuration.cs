using Autofac;
using Microsoft.Extensions.Logging;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.ReadWorkers;
using TaxpayerAlerter.DAL.ReadWorkers.Base;
using TaxpayerAlerter.DAL.RestServices;
using TaxpayerAlerter.DAL.RestServices.Base;
using TaxpayerAlerter.DAL.WriteWorkers;
using TaxpayerAlerter.DAL.WriteWorkers.Base;

namespace TaxpayerAlerter.DAL.Registrators
{
    public static class Configuration
    {
        public static ContainerBuilder RegisterDAL(this ContainerBuilder builder)
        {
            builder.RegisterType<XLSXReadWorker>().As<IReadWorker<ClientDAO>>();

            builder.RegisterType<DOCXWriteWorker>().As<IWriteWorker<ClientDAO>>();
            builder.RegisterType<XLSXWriteWorker>().As<IWriteWorker<ClientDAO>>();

            builder.Register(c => new HttpClient()).SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            builder.RegisterType<ClientRestService>().As<IRestService<ClientDAO>>();

            return builder;
        }
    }
}
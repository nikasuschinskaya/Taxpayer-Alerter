using Autofac;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.ReadWorkers.Base;
using TaxpayerAlerter.DAL.WriteWorkers;
using TaxpayerAlerter.DAL.WriteWorkers.Base;

namespace TaxpayerAlerter.DAL.Registrators
{
    public static class Configuration
    {
        public static ContainerBuilder RegisterDAL(this ContainerBuilder builder)
        {
            builder.RegisterType<DOCXWriteWorker>().As<IReadWorker<ClientDAO>>();

            builder.RegisterType<DOCXWriteWorker>().As<IWriteWorker<ClientDAO>>();
            builder.RegisterType<XLSXWriteWorker>().As<IWriteWorker<ClientDAO>>();

            builder.Register(c => new HttpClient()).SingleInstance();

            return builder;
        }
    }
}
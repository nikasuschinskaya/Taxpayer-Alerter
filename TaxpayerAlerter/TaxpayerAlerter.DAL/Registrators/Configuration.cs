using Autofac;
using TaxpayerAlerter.DAL.ReadWorkers.Base;
using TaxpayerAlerter.DAL.WriteWorkers;
using TaxpayerAlerter.DAL.WriteWorkers.Base;

namespace TaxpayerAlerter.DAL.Registrators
{
    public static class Configuration
    {
        public static ContainerBuilder RegisterDAL(this ContainerBuilder builder)
        {
            builder.RegisterType<DOCXWriteWorker>().As<IReadWorker>();

            builder.RegisterType<DOCXWriteWorker>().As<IWriteWorker>();
            builder.RegisterType<XLSXWriteWorker>().As<IWriteWorker>();

            return builder;
        }
    }
}
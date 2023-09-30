using Autofac;
using TaxpayerAlerter.BLL.Workers;
using TaxpayerAlerter.DAL.ReadWorkers;
using TaxpayerAlerter.DAL.ReadWorkers.Base;
using TaxpayerAlerter.DAL.Registrators;
using TaxpayerAlerter.DAL.WriteWorkers;
using TaxpayerAlerter.DAL.WriteWorkers.Base;

namespace TaxpayerAlerter.BLL.Registrators
{
    public static class Configuration
    {
        public static ContainerBuilder RegisterBLL(this ContainerBuilder builder)
        {
            builder = builder.RegisterDAL();

            builder.RegisterType<Worker>()
                  .WithParameter((pi, c) => pi.ParameterType == typeof(IReadWorker),
                                 (pi, c) => c.Resolve<XLSXReadWorker>())
                  .WithParameter((pi, c) => pi.ParameterType == typeof(IWriteWorker),
                                 (pi, c) => c.Resolve<XLSXWriteWorker>())
                  .WithParameter((pi, c) => pi.ParameterType == typeof(IWriteWorker),
                                 (pi, c) => c.Resolve<DOCXWriteWorker>());

            return builder;
        }
    }
}

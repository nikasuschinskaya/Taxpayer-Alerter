using Autofac;
using Microsoft.Extensions.Logging;
using TaxpayerAlerter.BLL.Workers;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.ReadWorkers;
using TaxpayerAlerter.DAL.Registrators;
using TaxpayerAlerter.DAL.RestServices;
using TaxpayerAlerter.DAL.RestServices.Base;
using TaxpayerAlerter.DAL.WriteWorkers;

namespace TaxpayerAlerter.BLL.Registrators
{
    public static class Configuration
    {
        public static ContainerBuilder RegisterBLL(this ContainerBuilder builder)
        {
            builder = builder.RegisterDAL();

            builder.RegisterType<Worker>()
                   .WithParameter((pi, c) => pi.ParameterType == typeof(IRestService<ClientDAO>),
                                  (pi, c) => c.Resolve<ClientRestService>());

            builder.RegisterType<XLSXReadWorker>().AsSelf();
            builder.RegisterType<DOCXWriteWorker>().AsSelf();
            builder.RegisterType<XLSXWriteWorker>().AsSelf();

            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            return builder;
        }
    }
}

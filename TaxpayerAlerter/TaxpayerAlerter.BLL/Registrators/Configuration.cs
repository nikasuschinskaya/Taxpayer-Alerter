using Autofac;
using Autofac.Features.AttributeFilters;
using TaxpayerAlerter.BLL.Workers;
using TaxpayerAlerter.BLL.Workers.Base;
using TaxpayerAlerter.DAL.Registrators;

namespace TaxpayerAlerter.BLL.Registrators
{
    public static class Configuration
    {
        public static ContainerBuilder RegisterBLL(this ContainerBuilder builder)
        {
            builder.RegisterDAL();

            builder.RegisterType<Worker>().As<IWorker>().WithAttributeFiltering();

            return builder;
        }
    }
}

using Autofac;
using Autofac.Features.AttributeFilters;
using Microsoft.Extensions.Logging;
using TaxpayerAlerter.BLL.Workers;
using TaxpayerAlerter.BLL.Workers.Base;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.Readers;
using TaxpayerAlerter.DAL.Readers.Base;
using TaxpayerAlerter.DAL.Registrators;
using TaxpayerAlerter.DAL.RestServices;
using TaxpayerAlerter.DAL.RestServices.Base;
using TaxpayerAlerter.DAL.Writers;
using TaxpayerAlerter.DAL.Writers.Base;

namespace TaxpayerAlerter.BLL.Registrators
{
    public static class Configuration
    {
        public static ContainerBuilder RegisterBLL(this ContainerBuilder builder)
        {
            builder.RegisterDAL();

            builder.RegisterType<Worker>().As<IWorker>().WithAttributeFiltering();

            builder.RegisterType<ClientRestService>().As<IRestService<ClientDAO>>();

            builder.RegisterType<XLSXReader>().Keyed<IReader<ClientDAO>>("XLSXReader");
            builder.RegisterType<DOCXWriter>().Keyed<IWriter<ClientDAO>>("DOCXWriter");
            builder.RegisterType<XLSXWriter>().Keyed<IWriter<ClientDAO>>("XLSXWriter");

            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            return builder;
        }
    }
}

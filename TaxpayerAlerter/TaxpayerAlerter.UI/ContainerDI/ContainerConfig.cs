using Autofac;
using Microsoft.Extensions.Logging;
using Serilog;
using TaxpayerAlerter.BLL.Registrators;
using TaxpayerAlerter.BLL.Workers;
using TaxpayerAlerter.UI.ViewModels;

namespace TaxpayerAlerter.UI.ContainerDI
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder().RegisterBLL();

            builder.RegisterType<MainViewModel>();
            builder.RegisterType<Worker>().AsSelf();

            builder.Register<Serilog.ILogger>((c, p) => 
                                    new LoggerConfiguration()
                                        .WriteTo.File("app.log")
                                        .WriteTo.Console()
                                        .CreateLogger())
                   .SingleInstance();

            builder.Register((c, p) => new LoggerFactory().AddSerilog(c.Resolve<Serilog.ILogger>()))
                   .SingleInstance();

            builder.Register((c, p) => c.Resolve<ILoggerFactory>().CreateLogger<MainViewModel>())
                   .SingleInstance();

            builder.Register((c, p) => c.Resolve<ILoggerFactory>().CreateLogger<Worker>())
                   .SingleInstance();

            return builder.Build();
        }
    }
}
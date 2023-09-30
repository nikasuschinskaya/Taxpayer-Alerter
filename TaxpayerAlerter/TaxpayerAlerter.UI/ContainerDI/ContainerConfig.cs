﻿using Autofac;
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
            {
                return new LoggerConfiguration()
                    .WriteTo.File("app.log")
                    .WriteTo.Console()
                    .CreateLogger();
            }).SingleInstance();

            builder.Register<ILoggerFactory>((c, p) =>
            {
                var logger = c.Resolve<Serilog.ILogger>();
                var loggerFactory = new LoggerFactory().AddSerilog(logger);
                return loggerFactory;
            }).SingleInstance();

            builder.Register<ILogger<MainViewModel>>((c, p) =>
            {
                var loggerFactory = c.Resolve<ILoggerFactory>();
                return loggerFactory.CreateLogger<MainViewModel>();
            }).SingleInstance();

            builder.Register<ILogger<Worker>>((c, p) =>
            {
                var loggerFactory = c.Resolve<ILoggerFactory>();
                return loggerFactory.CreateLogger<Worker>();
            }).SingleInstance();

            return builder.Build();
        }
    }
}

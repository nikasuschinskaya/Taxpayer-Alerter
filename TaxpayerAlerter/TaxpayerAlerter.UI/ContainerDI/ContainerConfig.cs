﻿using Autofac;

namespace TaxpayerAlerter.UI.ContainerDI
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();



            return builder.Build();
        }
    }
}

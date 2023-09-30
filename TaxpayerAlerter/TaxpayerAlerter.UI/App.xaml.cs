using Autofac;
using System.Windows;
using TaxpayerAlerter.UI.ContainerDI;

namespace TaxpayerAlerter.UI
{
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Container = ContainerConfig.Configure();
        }
    }
}

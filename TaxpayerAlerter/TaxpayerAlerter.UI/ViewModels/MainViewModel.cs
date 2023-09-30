using Autofac;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxpayerAlerter.BLL.Workers;
using TaxpayerAlerter.UI.Commands;
using TaxpayerAlerter.UI.ViewModels.Base;

namespace TaxpayerAlerter.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly Worker _worker;
        private readonly ILogger<MainViewModel> _logger;

        // Надо ещё свойство для считывания даты из DatePicker  

        public ICommand OKButtonClick { get; set; }
        public DateTime DateEnd { get; set; }

        public MainViewModel()
        {
            using (var scope = App.Container.BeginLifetimeScope())
            {
                _logger = scope.Resolve<ILogger<MainViewModel>>();
                _worker = scope.Resolve<Worker>();
            }

            DateEnd = DateTime.Now;
            OKButtonClick = new RelayCommand(OnOKButtonClicked);
        }

        private async void OnOKButtonClicked(object parameter)
        {
            _logger.LogInformation("Пользователь нажал на кнопку ОК");
            await _worker.StartWorkAsync();
        }
    }
}

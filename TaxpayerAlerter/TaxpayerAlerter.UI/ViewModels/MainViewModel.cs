using Autofac;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaxpayerAlerter.BLL.Workers.Base;
using TaxpayerAlerter.UI.Commands;
using TaxpayerAlerter.UI.ViewModels.Base;

namespace TaxpayerAlerter.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IWorker _worker;
        private readonly ILogger<MainViewModel> _logger;

        public ICommand OKButtonClick { get; set; }
        public DateTime SelectedDate { get; set; }
        public DateTime DateEnd { get; set; }

        public MainViewModel()
        {
            using (var scope = App.Container.BeginLifetimeScope())
            {
                _logger = scope.Resolve<ILogger<MainViewModel>>();
                _worker = scope.Resolve<IWorker>();
            }
            SelectedDate = DateTime.Now;
            DateEnd = DateTime.Now;
            OKButtonClick = new RelayCommand(OnOKButtonClicked);
        }

        private async void OnOKButtonClicked(object parameter)
        {
            _logger.LogInformation("Пользователь нажал на кнопку ОК");
            await ProcessClientDataAsync();
        }

        private async Task ProcessClientDataAsync()
        {
            var selectedDate = SelectedDate;
            _logger.LogInformation("Начат процесс обработки данных клиентов.");

            try
            {
                await _worker.StartWorkAsync(selectedDate);
                MessageBox.Show(_worker.GetResult());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Произошла ошибка при обработке данных: {ex.Message}");
                MessageBox.Show("Произошла ошибка при обработке данных!");
            }
        }
    }
}
using Autofac.Features.AttributeFilters;
using Microsoft.Extensions.Logging;
using TaxpayerAlerter.BLL.Helpers;
using TaxpayerAlerter.BLL.Workers.Base;
using TaxpayerAlerter.DAL.Enums;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.Readers.Base;
using TaxpayerAlerter.DAL.RestServices.Base;
using TaxpayerAlerter.DAL.Writers.Base;

namespace TaxpayerAlerter.BLL.Workers
{
    public class Worker : IWorker
    {
        private readonly IReader<ClientDAO> _xlsxReader;
        private readonly IWriter<ClientDAO> _docxWriter;
        private readonly IWriter<ClientDAO> _xlsxWriter;
        private readonly IRestService<ClientDAO> _clientRestService;
        private readonly ILogger<Worker> _logger;

        private string? _result;

        public Worker([KeyFilter("XLSXReader")] IReader<ClientDAO> xlsxReader,
                      [KeyFilter("DOCXWriter")] IWriter<ClientDAO> docWriter,
                      [KeyFilter("XLSXWriter")] IWriter<ClientDAO> xlsxWriter,
                      IRestService<ClientDAO> clientRestService,
                      ILogger<Worker> logger)
        {
            _xlsxReader = xlsxReader;
            _docxWriter = docWriter;
            _xlsxWriter = xlsxWriter;
            _clientRestService = clientRestService;
            _logger = logger;
        }

        public async Task StartWorkAsync(DateTime selectedDate)
        {
            if (!CheckInternetConnectionHelper.IsInternetConnected())
            {
                _result = "У вас нестабильное интернет-подключение или интернет отключен.\nПожалуйста, проверьте свое подключение к Интернету.";
                return;
            }

            var clients = await _xlsxReader.Read();

            foreach (var client in clients)
            {
                if (selectedDate < client.Date)
                {
                    client.Status = Status.None;
                    client.Unp = "-";
                    continue;
                }

                var newClient = await _clientRestService.GetClientByName(client.Name);

                client.Unp = newClient.Unp;
                client.State = newClient.State;
                client.Status = newClient.Status;
                client.FullName = newClient.FullName;

                _logger.LogInformation($"Обработан клиент с датой {client.Date}");
            }

            _logger.LogInformation("Идет запись всех клиентов в Excel файл");
            await _xlsxWriter.Write(clients.ToList());

            _logger.LogInformation("Идет клиентов в Doc файл");
            await _docxWriter.Write(clients
                                        .Where(client => client.Status == Status.Passed || client.Status == Status.ManualCheck)
                                        .ToList());
            _result = "Готово!";
        }

        public string GetResult() => _result;
    }
}
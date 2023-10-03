using Microsoft.Extensions.Logging;
using TaxpayerAlerter.DAL.Enums;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.ReadWorkers;
using TaxpayerAlerter.DAL.RestServices.Base;
using TaxpayerAlerter.DAL.WriteWorkers;

namespace TaxpayerAlerter.BLL.Workers
{
    public class Worker
    {
        private readonly XLSXReadWorker _xlsxReadWorker;
        private readonly DOCXWriteWorker _docWriteWorker;
        private readonly XLSXWriteWorker _xlsxWriteWorker;
        private readonly IRestService<ClientDAO> _clientRestService;
        private readonly ILogger<Worker> _logger;

        private string? _result;

        public Worker(XLSXReadWorker xlsxReadWorker,
                      DOCXWriteWorker docWriteWorker,
                      XLSXWriteWorker xlsxWriteWorker,
                      IRestService<ClientDAO> clientRestService,
                      ILogger<Worker> logger)
        {
            _xlsxReadWorker = xlsxReadWorker;
            _docWriteWorker = docWriteWorker;
            _xlsxWriteWorker = xlsxWriteWorker;
            _clientRestService = clientRestService;
            _logger = logger;
        }

        public async Task StartWorkAsync(DateTime selectedDate)
        {
            var clients = await _xlsxReadWorker.Read();

            foreach (var client in clients)
            {
                if (selectedDate >= client.Date)
                {
                    var newClient = await _clientRestService.PostRequest(client.Name);

                    client.Unp = newClient.Unp;
                    client.State = newClient.State;
                    client.Status = newClient.Status;
                    client.FullName = newClient.FullName;

                    _logger.LogInformation($"Обработан клиент с датой {client.Date}");
                }
                else
                {
                    client.Status = Status.None;
                    client.Unp = "-";
                }
            }
            _logger.LogInformation("Идет запись всех клиентов в Exel файл");
            await _xlsxWriteWorker.Write(clients.ToList());

            _logger.LogInformation("Идет клиентов в Doc файл");
            await _docWriteWorker.Write(clients
                                        .Where(client => client.Status == Status.Passed || client.Status == Status.ManualCheck)
                                        .ToList());
            _result = "Готово!";
        }

        public string GetResult() => _result;
    }
}

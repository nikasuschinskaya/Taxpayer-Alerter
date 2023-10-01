using Microsoft.Extensions.Logging;
using System.Net;
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
            var newClients = new List<ClientDAO>();
            var clientsForDoc = new List<ClientDAO>();
            var clients = _xlsxReadWorker.Read();

            //clients.Where(c => c.Date <= selectedDate).Select(c => newClients.Add(_clientRestService.PostRequest(c.Name).Result));
            foreach (var client in clients)
            {
                if (selectedDate >= client.Date)
                {
                    ClientDAO newClient = _clientRestService.PostRequest(client.Name).Result;
                   
                    newClients.Add(newClient);
                    _logger.LogInformation($"Обработан клиент с датой {client.Date}");
                }
            }
            _logger.LogInformation("Идет запись всех клинтов в Exel файл");
            _xlsxWriteWorker.Write(newClients);

            foreach (var client in newClients)
            {
                if (client.Status != DAL.Enums.Status.Error) clientsForDoc.Add(client);
            }

            _logger.LogInformation("Идет запись всех клинтов в Doc файл");
            _docWriteWorker.Write(clientsForDoc);
            _result = "Готово!";
        }

        public string GetResult() => _result;
    }
}

using IronXL;
using System.Configuration;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.ReadWorkers.Base;

namespace TaxpayerAlerter.DAL.ReadWorkers
{
    public class XLSXReadWorker : IReadWorker<ClientDAO>
    {
        private string _xlsxPath = ConfigurationManager.AppSettings["xlsxPath"].ToString();

        public IEnumerable<ClientDAO> Read()
        {
            WorkBook workBook = WorkBook.Load(_xlsxPath);
            WorkSheet workSheet = workBook.WorkSheets.First();

            List<ClientDAO> clients = new();

            for (var i = 2; i <= 10; i++)
            {
                var range = workSheet[$"A{i}:D{i}"].ToList();
                var client = new ClientDAO
                {
                    Name = range[0].Value.ToString(),
                    Date = DateTime.Parse(range[2].Value.ToString()),
                    Sum = int.Parse(range[3].Value.ToString())
                };
                clients.Add(client);
            }

            return clients;
        }
    }
}

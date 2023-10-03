using IronXL;
using System.Configuration;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.Readers.Base;

namespace TaxpayerAlerter.DAL.Readers
{
    public class XLSXReader : IReader<ClientDAO>
    {
        private readonly string _xlsxPath = ConfigurationManager.AppSettings["xlsxPath"].ToString();

        public async Task<IEnumerable<ClientDAO>> Read()
        {
            var workBook = WorkBook.Load(_xlsxPath);
            var workSheet = workBook.WorkSheets.First();

            var clients = new List<ClientDAO>();

            for (var i = 2; i <= 10; i++)
            {
                var range = workSheet[$"A{i}:D{i}"].ToList();
                var client = new ClientDAO
                {
                    Name = range[0].Value.ToString(),
                    Date = DateTime.Parse(range[2].Value.ToString()),
                    Sum = int.Parse(range[3].Value.ToString())
                };
                await Task.Run(() => clients.Add(client));
            }
            return clients;
        }
    }
}

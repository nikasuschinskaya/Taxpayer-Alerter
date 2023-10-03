using IronXL;
using System.Configuration;
using TaxpayerAlerter.DAL.Helpers;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.Writers.Base;

namespace TaxpayerAlerter.DAL.Writers
{
    public class XLSXWriter : IWriter<ClientDAO>
    {
        private readonly string _xlsxPath = ConfigurationManager.AppSettings["xlsxPath"].ToString();

        public async Task Write(List<ClientDAO> clients)
        {
            var workBook = WorkBook.Load(_xlsxPath);
            var workSheet = workBook.DefaultWorkSheet;

            for (int i = 0; i < clients.Count; i++)
            {
                workSheet[$"B{i + 2}"].Value = clients[i]?.Unp;
                workSheet[$"E{i + 2}"].Value = StatusHelper.GetStatus(clients[i].Status);
            }

            await Task.Run(() => workBook.Save());
        }
    }
}

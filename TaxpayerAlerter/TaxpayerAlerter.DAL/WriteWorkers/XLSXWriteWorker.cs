using IronXL;
using System.Configuration;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.WriteWorkers.Base;

namespace TaxpayerAlerter.DAL.WriteWorkers
{
    public class XLSXWriteWorker : IWriteWorker<ClientDAO>
    {
        private string _xlsxPath = ConfigurationManager.AppSettings["xlsxPath"].ToString();
        public void Write(List<ClientDAO> clients)
        {
            WorkBook workBook = WorkBook.Load(_xlsxPath);
            WorkSheet workSheet = workBook.DefaultWorkSheet;

            for (int i = 0; i < clients.Count; i++)
            {
                workSheet[$"B{i + 2}"].Value = clients[i].Unp;
                workSheet[$"E{i + 2}"].Value = clients[i].Status.ToString();
            }

            workBook.Save();
        }
    }
}

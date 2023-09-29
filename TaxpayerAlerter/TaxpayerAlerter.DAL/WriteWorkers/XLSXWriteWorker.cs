using IronXL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.WriteWorkers.Base;

namespace TaxpayerAlerter.DAL.WriteWorkers
{
    public class XLSXWriteWorker : IWriteWorker
    {
        private string _xlsxPath = ConfigurationManager.AppSettings["xlsxPath"].ToString();
        public void Write(List<Client> clients)
        {
            WorkBook workBook = WorkBook.Load(_xlsxPath);
            WorkSheet workSheet = workBook.DefaultWorkSheet;

            for (int i = 0; i < clients.Count; i++)
            {
                workSheet[$"B{i + 2}"].Value = clients[i].Unp.ToString();
                workSheet[$"E{i + 2}"].Value = clients[i].Status.ToString();
            }

            workBook.Save();
        }

        public void Write(Client client)
        {
            throw new NotImplementedException();
        }
    }
}

using IronXL;
using System.Configuration;
using TaxpayerAlerter.DAL.Enums;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.WriteWorkers.Base;

namespace TaxpayerAlerter.DAL.WriteWorkers
{
    public class XLSXWriteWorker : IWriteWorker<ClientDAO>
    {
        private string _xlsxPath = ConfigurationManager.AppSettings["xlsxPath"].ToString();
        public async Task Write(List<ClientDAO> clients)
        {
            WorkBook workBook = WorkBook.Load(_xlsxPath);
            WorkSheet workSheet = workBook.DefaultWorkSheet;

            for (int i = 0; i < clients.Count(); i++)
            {
                workSheet[$"B{i + 2}"].Value = clients[i]?.Unp;
                workSheet[$"E{i + 2}"].Value = GetStatus(clients[i].Status);
            }

            await Task.Run(() => workBook.Save());
        }

        private string GetStatus(Status status) => status switch
        {
            Status.None => "Не присвоен",
            Status.Passed => "Выполнено",
            Status.ManualCheck => "Ручная проверка",
            Status.Error => "Ошибка",
            _ => "Не присвоен",
        };
    }
}

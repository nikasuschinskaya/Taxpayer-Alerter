namespace TaxpayerAlerter.BLL.Workers.Base
{
    public interface IWorker
    {
        Task StartWorkAsync(DateTime selectedDate);
        string GetResult();
    }
}

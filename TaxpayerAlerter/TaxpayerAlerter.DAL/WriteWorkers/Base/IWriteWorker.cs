namespace TaxpayerAlerter.DAL.WriteWorkers.Base
{
    public interface IWriteWorker<T>
    {
        Task Write(List<T> client);
    }
}

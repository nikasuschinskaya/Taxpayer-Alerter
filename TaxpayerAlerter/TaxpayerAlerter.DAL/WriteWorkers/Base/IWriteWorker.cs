namespace TaxpayerAlerter.DAL.WriteWorkers.Base
{
    public interface IWriteWorker<T>
    {
        void Write(List<T> client);
    }
}

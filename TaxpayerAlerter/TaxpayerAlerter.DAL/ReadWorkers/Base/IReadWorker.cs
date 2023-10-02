namespace TaxpayerAlerter.DAL.ReadWorkers.Base
{
    public interface IReadWorker<T>
    {
        Task<IEnumerable<T>> Read();
    }
}

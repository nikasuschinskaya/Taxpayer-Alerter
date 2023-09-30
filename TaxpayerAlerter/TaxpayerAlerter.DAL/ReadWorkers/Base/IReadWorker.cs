namespace TaxpayerAlerter.DAL.ReadWorkers.Base
{
    public interface IReadWorker<T>
    {
        IEnumerable<T> Read();
    }
}

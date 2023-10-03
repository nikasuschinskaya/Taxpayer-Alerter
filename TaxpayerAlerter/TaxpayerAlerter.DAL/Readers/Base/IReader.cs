namespace TaxpayerAlerter.DAL.Readers.Base
{
    public interface IReader<T>
    {
        Task<IEnumerable<T>> Read();
    }
}

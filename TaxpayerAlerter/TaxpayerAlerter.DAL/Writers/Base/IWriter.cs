namespace TaxpayerAlerter.DAL.Writers.Base
{
    public interface IWriter<T>
    {
        Task Write(List<T> client);
    }
}

namespace TaxpayerAlerter.DAL.RestServices.Base
{
    public interface IRestService<T>
    {
        Task<string> PostRequest(T modelDAO);
    }
}

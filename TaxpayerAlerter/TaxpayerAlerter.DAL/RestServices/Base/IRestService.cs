namespace TaxpayerAlerter.DAL.RestServices.Base
{
    public interface IRestService<T>
    {
        Task<T> PostRequest(string name);
    }
}

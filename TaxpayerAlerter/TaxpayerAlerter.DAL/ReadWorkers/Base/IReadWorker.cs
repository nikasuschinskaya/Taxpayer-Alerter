using TaxpayerAlerter.DAL.ModelsDAO;

namespace TaxpayerAlerter.DAL.ReadWorkers.Base
{
    public interface IReadWorker
    {
        IEnumerable<Client> Read();
    }
}

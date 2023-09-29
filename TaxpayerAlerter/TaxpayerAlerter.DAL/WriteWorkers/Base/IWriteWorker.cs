using TaxpayerAlerter.DAL.ModelsDAO;

namespace TaxpayerAlerter.DAL.WriteWorkers.Base
{
    public interface IWriteWorker
    {
        void Write(List<Client> client);
    }
}

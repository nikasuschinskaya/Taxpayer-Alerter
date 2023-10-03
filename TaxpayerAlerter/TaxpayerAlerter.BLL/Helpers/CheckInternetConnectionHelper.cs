using System.Net.NetworkInformation;

namespace TaxpayerAlerter.BLL.Helpers
{
    public static class CheckInternetConnectionHelper
    {
        private const int TIMEOUT = 80;
        private const string ADDRESS = "grp.nalog.gov.by";

        public static bool IsInternetConnected()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send(ADDRESS, TIMEOUT);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (PingException)
            {
                return false;
            }
        }
    }
}

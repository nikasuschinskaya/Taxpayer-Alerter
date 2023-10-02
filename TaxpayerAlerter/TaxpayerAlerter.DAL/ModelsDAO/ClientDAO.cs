using Newtonsoft.Json;
using TaxpayerAlerter.DAL.Enums;

namespace TaxpayerAlerter.DAL.ModelsDAO
{
    public class ClientDAO
    {

        [JsonProperty("vunp")]
        public string? Unp { get; set; }

        public string? Name { get; set; }


        [JsonProperty("vnaimk")]
        public string? FullName { get; set; }

        public DateTime Date { get; set; }

        public int Sum { get; set; }

        [JsonProperty("vkods")]
        public string? State { get; set; }

        public Status Status { get; set; }
    }
}

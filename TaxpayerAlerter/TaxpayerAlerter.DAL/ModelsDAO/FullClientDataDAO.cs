using Newtonsoft.Json;

namespace TaxpayerAlerter.DAL.ModelsDAO
{
    public class FullClientDataDAO
    {
        [JsonProperty("vunp")]
        public string? Unp { get; set; }


        [JsonProperty("vnaimk")]
        public string? ShortName { get; set; }


        [JsonProperty("vnaimp")]
        public string? FullName { get; set; }


        [JsonProperty("nmns")]
        public int IMNSCode { get; set; }


        [JsonProperty("vmns")]
        public string? IMNSName { get; set; }


        [JsonProperty("dreg")]
        public DateTime RegistrationDate { get; set; }


        [JsonProperty("ckodsost")]
        public int StatusCode { get; set; }


        [JsonProperty("dlikv")]
        public DateTime? DateChangeStatus { get; set; }


        [JsonProperty("vlikv")]
        public string? ChangeBasis { get; set; }


        [JsonProperty("vpadres")]
        public string? Adress { get; set; }


        [JsonProperty("vkods")]
        public string? State { get; set; }


        [JsonProperty("active")]
        public string? PayerState { get; set; }
    }
}

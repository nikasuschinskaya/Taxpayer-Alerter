namespace TaxpayerAlerter.DAL.ModelsDAO
{
    public class FullClientDataDAO
    {
        public string? Unp { get; set; }
        public string? ShortName { get; set; }
        public string? FullName { get; set; }
        public int IMNSCode { get; set; }
        public string? IMNSName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int StatusCode { get; set; }
        public DateTime? DateChangeStatus { get; set; }
        public string? ChangeBasis { get; set; }
        public string? Adress { get; set; }
        public string? State { get; set; }
        public string? PayerState { get; set; }
    }
}

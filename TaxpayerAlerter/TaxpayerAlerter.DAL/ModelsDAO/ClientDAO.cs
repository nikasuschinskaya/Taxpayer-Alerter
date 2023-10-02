using System.Text.Json.Serialization;
using TaxpayerAlerter.DAL.Enums;

namespace TaxpayerAlerter.DAL.ModelsDAO
{
    public class ClientDAO
    {
        public string? Unp { get; set; }
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public DateTime Date { get; set; }
        public int Sum { get; set; }
        public string? State { get; set; }
        public Status Status { get; set; }
    }
}

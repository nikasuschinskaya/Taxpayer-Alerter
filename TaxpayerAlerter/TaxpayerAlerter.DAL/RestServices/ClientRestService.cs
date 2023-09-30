using Newtonsoft.Json;
using System.Text;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.RestServices.Base;

namespace TaxpayerAlerter.DAL.RestServices
{
    public class ClientRestService : IRestService<ClientDAO>
    {
        private readonly HttpClient _httpClient;

        public ClientRestService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<ClientDAO> PostRequest(string name)
        {
            var requestData = new
            {
                dfrom = (DateTime?)null,
                dto = (DateTime?)null,
                isduty = false,
                name = $"{name}",
                operation = "="
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://grp.nalog.gov.by/api/grp-public/search/payer", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var jsonResponse = JsonConvert.DeserializeObject<FullClientDataDAO>(responseString);

            return new ClientDAO { FullName = jsonResponse?.ShortName, Unp = jsonResponse?.Unp, State = jsonResponse?.State };
        }
    }
}
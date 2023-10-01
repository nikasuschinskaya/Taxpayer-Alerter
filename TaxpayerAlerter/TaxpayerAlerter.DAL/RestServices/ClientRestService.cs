using Newtonsoft.Json;
using System.Configuration;
using System.Text;
using TaxpayerAlerter.DAL.Enums;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.RestServices.Base;

namespace TaxpayerAlerter.DAL.RestServices
{
    public class ClientRestService : IRestService<ClientDAO>
    {
        private readonly HttpClient _httpClient;
        private List<FullClientDataDAO> _jsonResponse = new();
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

            var response = await _httpClient.PostAsync(ConfigurationManager.AppSettings["urlNalogGovBy"], content);

            var responseString = await response.Content.ReadAsStringAsync();


            if (responseString != "[]" || responseString != string.Empty) _jsonResponse = JsonConvert.DeserializeObject<List<FullClientDataDAO>>(responseString);
            else return new ClientDAO { Status = Status.Error };

            if(_jsonResponse?.Count > 1) 
                return new ClientDAO { FullName = _jsonResponse[0]?.ShortName, Unp = _jsonResponse?[0].Unp, State = _jsonResponse?[0].State, Status = Status.ManualCheck };
            if (_jsonResponse?[0].State != "Действующий" || _jsonResponse[0].Unp == null)
                return new ClientDAO { Status = Status.Error }; 

            return new ClientDAO { FullName = _jsonResponse[0]?.ShortName, Unp = _jsonResponse?[0].Unp, State = _jsonResponse?[0].State, Status = Status.Passed };
        }
    }
}
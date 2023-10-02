using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxpayerAlerter.DAL.Enums;
using TaxpayerAlerter.DAL.ModelsDAO;
using TaxpayerAlerter.DAL.RestServices.Base;

namespace TaxpayerAlerter.DAL.RestServices
{
    public class ClientRestService : IRestService<ClientDAO>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ClientRestService> _logger;

        public ClientRestService(HttpClient httpClient, ILogger<ClientRestService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ClientDAO> PostRequest(string name)
        {
            _logger.LogInformation($"Отправка запроса для клиента {name}");

            try
            {
                var requestData = new
                {
                    dfrom = (DateTime?)null,
                    dto = (DateTime?)null,
                    isduty = false,
                    name = name,
                    operation = "="
                };

                var jsonRequest = JsonConvert.SerializeObject(requestData);

                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://grp.nalog.gov.by/api/grp-public/search/payer", content);
                _logger.LogInformation($"Response: {response.StatusCode.ToString()}");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Ответ от сервера : {responseString}");

                    if (string.IsNullOrWhiteSpace(responseString) || responseString == "[]")
                    {
                        return new ClientDAO { Status = Status.Error };
                    }
                    else
                    {
                        var jsonResponse = JsonConvert.DeserializeObject<List<FullClientDataDAO>>(responseString);

                        if (jsonResponse.Count == 1)
                        {
                            if (jsonResponse[0].State != "Действующий" || jsonResponse[0].Unp == null)
                            {
                                return new ClientDAO { FullName = jsonResponse[0].ShortName, Status = Status.Error };
                            }
                            return new ClientDAO { FullName = jsonResponse[0].ShortName, Unp = jsonResponse[0].Unp, State = jsonResponse[0].State, Status = Status.Passed };
                        }
                        else if(jsonResponse.Count > 1)
                        {
                            return new ClientDAO { FullName = jsonResponse[0].ShortName, Unp = jsonResponse[0].Unp, State = jsonResponse[0].State, Status = Status.ManualCheck };
                        }
                        else return new ClientDAO { FullName = jsonResponse[0].ShortName, Status = Status.Error };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Произошла ошибка: {ex.Message}");
            }

            return new ClientDAO { Status = Status.Error };
        }
    }
}


//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System.Configuration;
//using System.Text;
//using TaxpayerAlerter.DAL.Enums;
//using TaxpayerAlerter.DAL.ModelsDAO;
//using TaxpayerAlerter.DAL.RestServices.Base;

//namespace TaxpayerAlerter.DAL.RestServices
//{
//    public class ClientRestService : IRestService<ClientDAO>
//    {
//        private readonly HttpClient _httpClient;
//        private readonly ILogger<ClientRestService> _logger;
//        private List<FullClientDataDAO> _jsonResponse = new();

//        public ClientRestService(HttpClient httpClient, ILogger<ClientRestService> logger)
//        {
//            _httpClient = httpClient;
//            _logger = logger;
//        }

//        public async Task<ClientDAO> PostRequest(string name)
//        {
//            _logger.LogInformation($"Отправка запроса для клиента {name}");
//            var requestData = new
//            {
//                dfrom = (DateTime?)null,
//                dto = (DateTime?)null,
//                isduty = false,
//                name = $"{name}",
//                operation = "="
//            };

//            var jsonRequest = JsonConvert.SerializeObject(requestData);
//            _logger.LogInformation($"JsonRequest: {jsonRequest}");

//            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
//            _logger.LogInformation($"Content: {content.ToString()}");

//            var response = await _httpClient.PostAsync("http://grp.nalog.gov.by/api/grp-public/search/payer"/*ConfigurationManager.AppSettings["urlNalogGovBy"]?.ToString()*/, content);
//            _logger.LogInformation($"Response: {response.StatusCode.ToString()}");

//            var responseString = await response.Content.ReadAsStringAsync();
//            _logger.LogInformation($"Ответ от сервера : {responseString}");

//            if (responseString != "[]" || responseString != string.Empty) _jsonResponse = JsonConvert.DeserializeObject<List<FullClientDataDAO>>(responseString);
//            else return new ClientDAO { Status = Status.Error };

//            if (_jsonResponse?.Count > 1)
//                return new ClientDAO { FullName = _jsonResponse[0]?.ShortName, Unp = _jsonResponse?[0].Unp, State = _jsonResponse?[0].State, Status = Status.ManualCheck };
//            if (_jsonResponse?[0].State != "Действующий" || _jsonResponse[0].Unp == null)
//                return new ClientDAO { Status = Status.Error };

//            return new ClientDAO { FullName = _jsonResponse[0]?.ShortName, Unp = _jsonResponse?[0].Unp, State = _jsonResponse?[0].State, Status = Status.Passed };
//        }
//    }
//}
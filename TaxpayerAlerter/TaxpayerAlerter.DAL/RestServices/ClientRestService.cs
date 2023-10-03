using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Configuration;
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

                var response = await _httpClient.PostAsync(ConfigurationManager.AppSettings["urlNalogGovBy"]?.ToString(), content);
                _logger.LogInformation($"Response: {response.StatusCode}");

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
                        var jsonResponse = JsonConvert.DeserializeObject<List<ClientDAO>>(responseString);

                        if (jsonResponse.Count == 1)
                        {
                            if (jsonResponse[0].State != "Действующий" || jsonResponse[0].Unp == null)
                            {
                                return new ClientDAO { FullName = jsonResponse[0].FullName, Status = Status.Error };
                            }
                            return new ClientDAO { FullName = jsonResponse[0].FullName, Unp = jsonResponse[0].Unp, State = jsonResponse[0].State, Status = Status.Passed };
                        }
                        else if(jsonResponse.Count > 1)
                        {
                            if (jsonResponse[0].State != "Действующий" || jsonResponse[0].Unp == null)
                            {
                                return new ClientDAO { FullName = jsonResponse[0].FullName, Status = Status.Error };
                            }
                            return new ClientDAO { FullName = jsonResponse[0].FullName, Unp = jsonResponse[0].Unp, State = jsonResponse[0].State, Status = Status.ManualCheck };
                        }
                        else return new ClientDAO { FullName = jsonResponse[0].FullName, Status = Status.Error };
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
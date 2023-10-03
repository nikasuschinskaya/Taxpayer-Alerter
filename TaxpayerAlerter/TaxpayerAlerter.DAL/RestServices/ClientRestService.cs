using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ClientRestService> _logger;

        public ClientRestService(HttpClient httpClient, ILogger<ClientRestService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ClientDAO> GetClientByName(string name)
        {
            _logger.LogInformation($"Отправка запроса для клиента {name}");

            var clientError = new ClientDAO { Status = Status.Error };

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
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Запрос вернул ошибку");
                    return clientError;
                }

                var responseString = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Ответ от сервера : {responseString}");

                if (string.IsNullOrWhiteSpace(responseString))
                {
                    return clientError;
                }

                var jsonResponse = JsonConvert.DeserializeObject<List<ClientDAO>>(responseString);
                if (jsonResponse?.Count == 0)
                {
                    return clientError;
                }

                var firstResponse = jsonResponse[0];
                if (firstResponse.State != "Действующий" || firstResponse.Unp == null)
                {
                    return clientError;
                }

                var successClient = new ClientDAO
                {
                    FullName = firstResponse.FullName,
                    Unp = firstResponse.Unp,
                    State = firstResponse.State
                };

                successClient.Status = jsonResponse.Count == 1 ? Status.Passed : Status.ManualCheck;

                return successClient;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Произошла ошибка: {ex.Message}");
                return clientError;
            }
        }
    }
}
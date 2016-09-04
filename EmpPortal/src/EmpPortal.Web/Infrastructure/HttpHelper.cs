using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVCApp.Infrastructure
{
    public class HttpHelper
    {
        static HttpClient _httpClient = new HttpClient();
        static ILogger _httpLogger;

        static HttpHelper()
        {
            _httpLogger = ApplicationServices.LoggerFactory.CreateLogger<HttpHelper>();
            string baseUrl = ApplicationServices.Configuration["ApiConnection:BaseUrl"];
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpLogger.LogInformation("API Base url - " + baseUrl);
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            _httpLogger.LogInformation("Url being fired: " + url);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            var responseMessage = await _httpClient.SendAsync(request);

            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        public static async Task<bool> PostAsync<T>(string url, T requestBody)
        {
            _httpLogger.LogInformation("Url being fired: " + url);
            // HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            var responseMessage =  await _httpClient.PostAsJsonAsync(url, requestBody);

            return responseMessage.IsSuccessStatusCode;
        }
    }
}

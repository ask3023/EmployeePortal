using Autofac;
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
    public class HttpHelper : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly IEmpLogger _httpLogger;

        public HttpHelper()
        {
            var accessor = ApplicationServices.Container.Resolve<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            var resolved = (ILogIdentifier)context.RequestServices.GetService(typeof(ILogIdentifier));

            var loggerFactory = ApplicationServices.LoggerFactory;
                _httpLogger = new EmpLogger(
                    loggerFactory.CreateLogger<HttpHelper>(),
                    resolved
                    );

            string baseUrl = ApplicationServices.Configuration["ApiConnection:BaseUrl"];
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpLogger.LogInformation("API Base url - " + baseUrl);
        }

        public async Task<HttpResponse<T>> GetAsync<T>(string url)
        {
            _httpLogger.LogInformation("Url being fired: " + url);
            var response = new HttpResponse<T>();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            var responseMessage = await _httpClient.SendAsync(request);
            response.StatusCode = responseMessage.StatusCode;
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                response.ResponseModel = JsonConvert.DeserializeObject<T>(responseContent);
            }
            else
            {
                response.ErrorMessage = responseContent;
            }

            return response;
        }

        public async Task<HttpResponse<bool>> PostAsync<T>(string url, T requestBody)
        {
            _httpLogger.LogInformation("Url being fired: " + url);
            // HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            var responseMessage =  await _httpClient.PostAsJsonAsync(url, requestBody);
            var response = new HttpResponse<bool>();
            response.StatusCode = responseMessage.StatusCode;
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                response.ResponseModel = true;
            }
            else
            {
                response.ErrorMessage = responseContent;
            }

            return response;
        }

        ~HttpHelper()
        {
            if(_httpClient != null)
            {
                _httpClient.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            if(_httpClient != null)
            {
                _httpClient.Dispose();
            }
        }
    }
}

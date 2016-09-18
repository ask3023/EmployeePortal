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
        private ILogIdentifier _logIdentifier;

        public HttpHelper()
        {
            var httpContextAccessor = ApplicationServices.Container.Resolve<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;
            _logIdentifier = (ILogIdentifier)httpContext.RequestServices.GetService(typeof(ILogIdentifier));

            var loggerFactory = ApplicationServices.LoggerFactory;
                _httpLogger = new EmpLogger(
                    loggerFactory.CreateLogger<HttpHelper>(),
                    _logIdentifier
                    );

            string baseUrl = ApplicationServices.Configuration["ApiConnection:BaseUrl"];
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("X-LogIdentifier", _logIdentifier.LogId.ToString());
            
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

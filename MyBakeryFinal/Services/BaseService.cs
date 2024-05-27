using Common.CommConstants;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace MyBakeryFinal.Services
{
    public abstract class BaseService
    {
        JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        private readonly string requestUri;

        public BaseService(string controllerName, int timeoutSec = 30)
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri($"https://localhost:7165/api/");
            HttpClient.Timeout = TimeSpan.FromSeconds(timeoutSec);
            requestUri = controllerName;
        }

        protected HttpClient HttpClient { get; }

        public virtual async Task<T> PostAsync<T>(IRequest request)
        {
            StringContent requestContent = null;

            if (request.ContainsData)
            {
                string jsonResult = JsonSerializer.Serialize(request, request.GetType(), serializerOptions);
                requestContent = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage httpResponse = await HttpClient.PostAsync(requestUri, requestContent);
            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseBody, serializerOptions);
        }

        public virtual async Task<T> GetSearchAsync<T>(string filter, string searchWord)
        {
            HttpResponseMessage httpResponse = await HttpClient.GetAsync($"{requestUri}/search/{filter}/{searchWord}");
            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseBody, serializerOptions);
        }

        public virtual async Task<T> GetAsync<T>(string id)
        {
            HttpResponseMessage httpResponse = await HttpClient.GetAsync($"{requestUri}/{id}");
            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseBody, serializerOptions);
        }

        public virtual async Task<T> GetAllAsync<T>()
        {
            HttpResponseMessage httpResponse = await HttpClient.GetAsync(requestUri);
            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseBody, serializerOptions);
        }

        public virtual async Task<T> PutAsync<T>(int id, IRequest request)
        {
            StringContent requestContent = null;

            if (request.ContainsData)
            {
                string jsonResult = JsonSerializer.Serialize(request, request.GetType(), serializerOptions);
                requestContent = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage httpResponse = await HttpClient.PutAsync($"{requestUri}/{id}", requestContent);
            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseBody, serializerOptions);
        }

        public virtual async Task<T> DeleteAsync<T>(string id)
        {
            HttpResponseMessage httpResponse = await HttpClient.DeleteAsync($"{requestUri}/{id}");
            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseBody, serializerOptions);
        }
    }
}

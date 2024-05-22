﻿using Common.CommConstants;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace MyBakeryFinal.Services
{
    public abstract class BaseService
    {
        JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public BaseService(string controllerName, int timeoutSec = 30)
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri($"https://localhost:7165/api/{controllerName}/");
            HttpClient.Timeout = TimeSpan.FromSeconds(timeoutSec);
        }

        protected HttpClient HttpClient { get; }

        public virtual async Task<T> SendRequest<T>(IRequest request)
        {
            StringContent requestContent = null;

            if (request.HasData)
            {
                string jsonResult = JsonSerializer.Serialize(request, request.GetType(), serializerOptions);
                requestContent = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage httpResponse = await HttpClient.PostAsync(request.EndpointName, requestContent);
            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            var temp = JsonSerializer.Deserialize<T>(responseBody, serializerOptions);
            return temp;
        }
    }
}

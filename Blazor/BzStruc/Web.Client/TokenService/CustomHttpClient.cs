using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Client.TokenService
{
    public static class CustomHttpClient
    {
        public static HttpClient AddHeader(this HttpClient http, string token)
        {
            http.DefaultRequestHeaders.Remove("Authorization");
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            return http;
        }

        private static async Task testc(this HttpClient http, string uri)
        {
            await http.GetJsonAsync<string>(uri);
        }
    }
}

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiHelper
{
    public class ApiHelper<T>
    {
        private readonly HttpClient _httpClient;

        public ApiHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GenericCallWebApiAsync(Uri uri)
        {
            //using var client = new HttpClient();
            T resourceList = default;

            // client.DefaultRequestHeaders.CacheControl.Public = true;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //GET Method  
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                resourceList = JsonConvert.DeserializeObject<T>(data);
            }

            return resourceList;
        }
    }
}
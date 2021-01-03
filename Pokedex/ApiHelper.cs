using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Pokedex
{
    public class ApiHelper<T>
    {
        private readonly HttpClient httpClient;

        public ApiHelper(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<T> GenericCallWebApiAsync(Uri uri)
        {
            //using var client = new HttpClient();
            T resourceList = default;

            // client.DefaultRequestHeaders.CacheControl.Public = true;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //GET Method  
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                resourceList = JsonConvert.DeserializeObject<T>(data);
            }
            return resourceList;
        }
    }
}

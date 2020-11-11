using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Pokedex
{
    static class ApiHelper<T>
    {
        public static async Task<T> GenericCallWebApiAsync(Uri uri)
        {
            using var client = new HttpClient();
            T resourceList = default;

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //GET Method  
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                resourceList = await response.Content.ReadAsAsync<T>();
            }
            return resourceList;
        }
    }
}

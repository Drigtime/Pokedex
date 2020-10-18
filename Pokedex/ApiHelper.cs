using Pokedex.PokeApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex
{
    class ApiHelper
    {
        private Uri BaseAddress;

        public ApiHelper(Uri baseAddress)
        {
            BaseAddress = baseAddress;
        }

        public async Task<NamedAPIResourceList> CallWebAPIAsync(string prefix)
        {
            using (var client = new HttpClient())
            {
                NamedAPIResourceList resourceList = null;

                client.BaseAddress = BaseAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method  
                HttpResponseMessage response = await client.GetAsync(prefix);
                if (response.IsSuccessStatusCode)
                {
                    resourceList = await response.Content.ReadAsAsync<NamedAPIResourceList>();
                }
                return resourceList;
            }
        }
    }
}

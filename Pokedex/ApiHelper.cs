using Pokedex.PokeApi;
using Pokedex.PokeApi.EndPoints;
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

        public async Task<Pokemon> CallWebAPIAsyncPokemon(string pokemonName)
        {
            using (var client = new HttpClient())
            {
                Pokemon resourceList = null;

                client.BaseAddress = BaseAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method  
                HttpResponseMessage response = await client.GetAsync(pokemonName);
                if (response.IsSuccessStatusCode)
                {
                    resourceList = await response.Content.ReadAsAsync<Pokemon>();
                }
                return resourceList;
            }
        }

        public async Task<EvolutionChain> CallWebAPIAsyncEvolutionChain(string pokemonId)
        {
            using (var client = new HttpClient())
            {
                EvolutionChain resourceList = null;

                client.BaseAddress = BaseAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method  
                HttpResponseMessage response = await client.GetAsync(pokemonId);
                if (response.IsSuccessStatusCode)
                {
                    resourceList = await response.Content.ReadAsAsync<EvolutionChain>();
                }
                return resourceList;
            }
        }
    }
}

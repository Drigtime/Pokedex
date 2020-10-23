using Pokedex.PokeApi.EndPoints;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Pokedex
{
    class ApiHelper
    {
        private readonly Uri _baseAddress;

        public ApiHelper(Uri baseAddress)
        {
            _baseAddress = baseAddress;
        }

        public async Task<NamedApiResourceList> CallWebApiAsync(string prefix)
        {
            using var client = new HttpClient();
            NamedApiResourceList resourceList = null;

            client.BaseAddress = _baseAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //GET Method  
            HttpResponseMessage response = await client.GetAsync(prefix);
            if (response.IsSuccessStatusCode)
            {
                resourceList = await response.Content.ReadAsAsync<NamedApiResourceList>();
            }
            return resourceList;
        }

        public async Task<Pokemon> CallWebApiAsyncPokemon(string id)
        {
            using var client = new HttpClient();
            Pokemon resourceList = null;

            client.BaseAddress = _baseAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //GET Method  
            HttpResponseMessage response = await client.GetAsync($"/api/v2/pokemon/{id}");
            if (response.IsSuccessStatusCode)
            {
                resourceList = await response.Content.ReadAsAsync<Pokemon>();
            }
            return resourceList;
        }

        public async Task<EvolutionChain> CallWebApiAsyncEvolutionChain(string id)
        {
            using var client = new HttpClient();
            EvolutionChain resourceList = null;

            client.BaseAddress = _baseAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //GET Method  
            HttpResponseMessage response = await client.GetAsync($"/api/v2/evolution-chain/{id}");
            if (response.IsSuccessStatusCode)
            {
                resourceList = await response.Content.ReadAsAsync<EvolutionChain>();
            }
            return resourceList;
        }

        public async Task<PokemonSpecies> CallWebApiAsyncPokemonSpecies(string id)
        {
            using var client = new HttpClient();
            PokemonSpecies resourceList = null;

            client.BaseAddress = _baseAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //GET Method  
            HttpResponseMessage response = await client.GetAsync($"/api/v2/pokemon-species/{id}");
            if (response.IsSuccessStatusCode)
            {
                resourceList = await response.Content.ReadAsAsync<PokemonSpecies>();
            }
            return resourceList;
        }
    }
}

using Pokedex.PokeApi.EndPoints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Pokedex
{
    public class AppMethods
    {
        public const string Left = "left";
        public const string Right = "right";
        public static readonly Uri BaseAddress = new Uri("https://pokeapi.co");
        public HttpClient httpClient;
        public AppMethods(HttpClient httpClient)
        {
           this.httpClient = httpClient;
        }

        public async Task<NamedApiResourceList> SetPokemonListView(NamedApiResourceList namedApiResourceList, ListView listView, string direction)
        {
            Uri uri = direction == Left ? namedApiResourceList.Previous : namedApiResourceList.Next;

            if (uri != null)
            {
                namedApiResourceList = await GetNamedApiResourceList(uri);
                await listView.SetSourceAsync(namedApiResourceList.Results);
            }

            return namedApiResourceList;
        }

        public async Task<NamedApiResourceList> SetPokemonListView(ListView listView)
        {
            NamedApiResourceList namedApiResourceList = await GetNamedApiResourceList(new Uri(BaseAddress, $"api/v2/pokemon?limit=20"));
            await listView.SetSourceAsync(namedApiResourceList.Results);
            return namedApiResourceList;
        }

        public async Task<NamedApiResourceList> GetNamedApiResourceList(Uri uri)
        {
            var apiHelper = new ApiHelper<NamedApiResourceList>(httpClient);
            NamedApiResourceList namedApiResourceList = await apiHelper.GenericCallWebApiAsync(uri);
            return namedApiResourceList;
        }

        public async Task<Pokemon> GetPokemon(Uri uri)
        {
            var apiHelper = new ApiHelper<Pokemon>(httpClient);
            return await apiHelper.GenericCallWebApiAsync(uri);
        }

        public async Task<List<LocationAreaEncounter>> GetPokemonLocationAreaEncounters(Uri uri)
        {
            var apiHelper = new ApiHelper<List<LocationAreaEncounter>>(httpClient);
            return await apiHelper.GenericCallWebApiAsync(uri);
        }

        public async Task<PokemonSpecies> GetPokemonSpecies(Uri uri)
        {
            var apiHelper = new ApiHelper<PokemonSpecies>(httpClient);
            return await apiHelper.GenericCallWebApiAsync(uri);
        }

        public async Task<EvolutionChain> GetPokemonEvolution(Uri uri)
        {
            var apiHelper = new ApiHelper<EvolutionChain>(httpClient);
            return await apiHelper.GenericCallWebApiAsync(uri);
        }

        //enumerate cache's keys & values
        public static void GetCacheEnumAsync(MemoryCache cache)
        {
            IDictionaryEnumerator cacheEnumerator = (IDictionaryEnumerator)((IEnumerable)cache).GetEnumerator();
            while (cacheEnumerator.MoveNext())
            {
                Debug.WriteLine("///////// Item de la liste du cache : \n\n{0} : {1}{2}", cacheEnumerator.Key, cacheEnumerator.Value, Environment.NewLine);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using ApiHelper;
using PokeApi;
using Terminal.Gui;

namespace Pokedex
{
    public class AppMethods
    {
        public const string Left = "left";
        public const string Right = "right";
        public static readonly Uri BaseAddress = new Uri("https://pokeapi.co");
        public HttpClient HttpClient;

        public AppMethods(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task SetPokemonListView(NamedApiResourceList namedApiResourceList, ListView listView,
            string direction)
        {
            var uri = direction == Left ? namedApiResourceList.Previous : namedApiResourceList.Next;

            if (uri != null)
            {
                namedApiResourceList = await GetNamedApiResourceList(uri);
                await listView.SetSourceAsync(namedApiResourceList.Results);
            }
        }

        public async Task<NamedApiResourceList> SetPokemonListView(ListView listView)
        {
            var namedApiResourceList = await GetNamedApiResourceList(new Uri(BaseAddress, "api/v2/pokemon?limit=20"));
            await listView.SetSourceAsync(namedApiResourceList.Results);
            return namedApiResourceList;
        }

        public async Task<NamedApiResourceList> GetNamedApiResourceList(Uri uri)
        {
            var apiHelper = new ApiHelper<NamedApiResourceList>(HttpClient);
            var namedApiResourceList = await apiHelper.GenericCallWebApiAsync(uri);
            return namedApiResourceList;
        }

        public async Task<Pokemon> GetPokemon(Uri uri)
        {
            var apiHelper = new ApiHelper<Pokemon>(HttpClient);
            return await apiHelper.GenericCallWebApiAsync(uri);
        }

        public async Task<List<LocationAreaEncounter>> GetPokemonLocationAreaEncounters(Uri uri)
        {
            var apiHelper = new ApiHelper<List<LocationAreaEncounter>>(HttpClient);
            return await apiHelper.GenericCallWebApiAsync(uri);
        }

        public async Task<PokemonSpecies> GetPokemonSpecies(Uri uri)
        {
            var apiHelper = new ApiHelper<PokemonSpecies>(HttpClient);
            return await apiHelper.GenericCallWebApiAsync(uri);
        }

        public async Task<EvolutionChain> GetPokemonEvolution(Uri uri)
        {
            var apiHelper = new ApiHelper<EvolutionChain>(HttpClient);
            return await apiHelper.GenericCallWebApiAsync(uri);
        }

        //enumerate cache's keys & values
        public static void GetCacheEnumAsync(MemoryCache cache)
        {
            var cacheEnumerator = (IDictionaryEnumerator) ((IEnumerable) cache).GetEnumerator();
            while (cacheEnumerator.MoveNext())
                Debug.WriteLine("///////// Item de la liste du cache : \n\n{0} : {1}{2}", cacheEnumerator.Key,
                    cacheEnumerator.Value, Environment.NewLine);
        }
    }
}
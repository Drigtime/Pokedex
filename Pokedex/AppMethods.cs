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
        public const string Right = "Right";
        private static HttpClient _httpClient;
        public static readonly Uri BaseAddress = new Uri("https://pokeapi.co");

        public AppMethods(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static async Task<NamedApiResourceList> GetPokemonList(NamedApiResourceList namedApiResourceList,
            string direction)
        {
            var uri = direction == Left ? namedApiResourceList.Previous : namedApiResourceList.Next;

            if (uri != null)
            {
                namedApiResourceList = await GetNamedApiResourceList(uri);
            }
            
            return namedApiResourceList;
        }

        public static async Task<NamedApiResourceList> GetPokemonList()
        {
            var namedApiResourceList = await GetNamedApiResourceList(new Uri(BaseAddress, "api/v2/pokemon?limit=20"));
            return namedApiResourceList;
        }

        private static async Task<NamedApiResourceList> GetNamedApiResourceList(Uri uri)
        {
            var apiHelper = new ApiHelper<NamedApiResourceList>(_httpClient);
            var namedApiResourceList = await apiHelper.GenericCallWebApiAsync(uri);
            return namedApiResourceList;
        }

        public static async Task<Pokemon> GetPokemon(Uri uri)
        {
            var apiHelper = new ApiHelper<Pokemon>(_httpClient);
            
            return await apiHelper.GenericCallWebApiAsync(uri);
        }

        public static async Task<List<LocationAreaEncounter>> GetPokemonLocationAreaEncounters(Uri uri)
        {
            var apiHelper = new ApiHelper<List<LocationAreaEncounter>>(_httpClient);
            return await apiHelper.GenericCallWebApiAsync(uri);
        }

        public static async Task<PokemonSpecies> GetPokemonSpecies(Uri uri)
        {
            var apiHelper = new ApiHelper<PokemonSpecies>(_httpClient);
            return await apiHelper.GenericCallWebApiAsync(uri);
        }

        public static async Task<EvolutionChain> GetPokemonEvolution(Uri uri)
        {
            var apiHelper = new ApiHelper<EvolutionChain>(_httpClient);
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
        
        public static async Task<NamedApiResourceList> PaginatePokemonList(int paginationCounter, NamedApiResourceList namedApiResourceList, string direction)
        {
            if (!MemoryCache.Default.Contains($"PokemonList{paginationCounter}"))
                MemoryCache.Default.Add($"PokemonList{paginationCounter}",
                    await GetPokemonList(namedApiResourceList, direction), DateTime.Now.AddHours(24));

            namedApiResourceList = (NamedApiResourceList) MemoryCache.Default.Get($"PokemonList{paginationCounter}");

            return namedApiResourceList;
        }
    }
}
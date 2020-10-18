using Pokedex.PokeApi;
using System;
using System.Threading.Tasks;

namespace Pokedex
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            ApiHelper apiHelper = new ApiHelper(new Uri("https://pokeapi.co/api/v2/"));
            NamedAPIResourceList resourceList = await apiHelper.CallWebAPIAsync("pokemon?limit=100&offset=200");

            Console.WriteLine(
                    @$"
                    Count: {resourceList.Count}
                    Next: {resourceList.Next}
                    Previous: {resourceList.Previous}
                    Result: [
                        name: {resourceList.Results[0].Name}
                        url: {resourceList.Results[0].Url}
                    ]"
                );
        }
    }
}

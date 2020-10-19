using Pokedex.PokeApi;
using System;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Pokedex
{
    class Program
    {
        static void Main(string[] args)
        {
            //interface de l'app
            Application.Run<App>();

            List().GetAwaiter().GetResult();
            ListAll().GetAwaiter().GetResult();
        }

        static async Task List()
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

        static async Task ListAll()
        {
            ApiHelper apiHelper = new ApiHelper(new Uri("https://pokeapi.co/api/v2/"));
            NamedAPIResourceList resourceList = await apiHelper.CallWebAPIAsync("pokemon");

            Console.WriteLine(
                    @$"
                    Count: {resourceList.Count}
                    Result: [
                        name: {resourceList.Results[0].Name}
                        url: {resourceList.Results[0].Url}
                    ]"
                );
        }
    }
}

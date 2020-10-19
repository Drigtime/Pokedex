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
        }

        public static async Task<NamedAPIResourceList> List(string prefix = "pokemon?limit=20")
        {
            ApiHelper apiHelper = new ApiHelper(new Uri("https://pokeapi.co/api/v2/"));
            NamedAPIResourceList resourceList = await apiHelper.CallWebAPIAsync(prefix);
            return resourceList;
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

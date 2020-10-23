using System;
using System.Threading.Tasks;
using Terminal.Gui;
using Pokedex.PokeApi.EndPoints;

namespace Pokedex
{
    class App : Window
    {
        private const string Left = "left";
        private const string Right = "right";
        private static readonly Uri Uri = new Uri("https://pokeapi.co");
        private static readonly ApiHelper ApiHelper = new ApiHelper(Uri);

        static App()
        {
            NamedApiResourceList namedApiResourceList = null;

            Application.Init();

            var top = new Toplevel()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var win = new Window("Pokedex")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var menu = new MenuBar(
                new[]
                {
                    //new MenuBarItem ("_File", new MenuItem [] {
                    new MenuBarItem("_Quit", "", () =>
                    {
                        Application.Shutdown();
                        Environment.Exit(0);
                        //})
                    }),
                });

            var pokemonListWindow = new Window("List")
            {
                X = 0,
                Y = 6,
                Width = Dim.Percent(50),
                Height = Dim.Fill()
            };

            var pokemonWindow = new Window("Pokemon")
            {
                X = Pos.Percent(50),
                Y = 6,
                Width = Dim.Percent(50),
                Height = Dim.Fill()
            };

            var pokemonNameLabel = new Label("")
            {
                X = Pos.Center(),
                Y = 1
            };

            var pokemonEvolutionLabel = new Label("")
            {
                X = Pos.Center(),
                Y = 3
            };

            var researchLabel = new Label("Recherche :")
            {
                X = 2,
                Y = 3
            };

            var research = new TextField("")
            {
                X = 2,
                Y = 4,
                Width = 25,
                Height = 1
            };

            var researchButton = new Button("Search") {X = Pos.Right(research) + 1, Y = 4};

            var previousButton = new Button("Previous") {X = 0, Y = Pos.AnchorEnd(1)};

            var nextButton = new Button("Next");
            nextButton.X = Pos.AnchorEnd() - (Pos.Right(nextButton) - Pos.Left(nextButton));
            nextButton.Y = Pos.AnchorEnd(1);


            var pokemonListView = new ListView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };


            pokemonListView.Initialized += (e, s) =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    namedApiResourceList = await SetPokemonListView(pokemonListView);
                });
            };

            pokemonListView.OpenSelectedItem += (e) =>
            {
                NamedApiResource namedApiResource = (NamedApiResource) e.Value;

                Application.MainLoop.Invoke(async () =>
                {
                    Pokemon pokemon = await GetPokemon(namedApiResource.Url.Segments[4]);
                    PokemonSpecies pokemonSpecies = await GetPokemonSpecies(pokemon.Species.Url.Segments[4]);
                    EvolutionChain evolutionChain = await GetPokemonEvolution(pokemonSpecies.EvolutionChain.Url.Segments[4]);

                    pokemonEvolutionLabel.Width = evolutionChain.Chain.EvolvesTo[0].Species.Name.Length;
                    pokemonEvolutionLabel.Text = evolutionChain.Chain.EvolvesTo[0].Species.Name;

                    pokemonNameLabel.Width = pokemon.Name.Length;
                    pokemonNameLabel.Text = pokemon.Name;
                });
            };

            previousButton.Clicked += () =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    namedApiResourceList =
                        await SetPokemonListView(namedApiResourceList, pokemonListView, Left);
                });
            };

            nextButton.Clicked += () =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    namedApiResourceList =
                        await SetPokemonListView(namedApiResourceList, pokemonListView, Right);
                });
            };

            pokemonWindow.Add(pokemonNameLabel, pokemonEvolutionLabel);
            pokemonListWindow.Add(previousButton, nextButton, pokemonListView);
            top.Add(menu, win, pokemonListWindow, pokemonWindow, research, researchLabel, researchButton);
            Application.Run(top);
        }

        private static async Task<NamedApiResourceList> SetPokemonListView(NamedApiResourceList namedApiResourceList,
            ListView listView, string direction)
        {
            Uri uri = direction == Left ? namedApiResourceList.Previous : namedApiResourceList.Next;

            if (uri != null)
            {
                namedApiResourceList = await GetNamedApiResourceList(uri.PathAndQuery);
                await listView.SetSourceAsync(namedApiResourceList.Results);
            }

            return namedApiResourceList;
        }

        private static async Task<NamedApiResourceList> SetPokemonListView(ListView listView)
        {
            NamedApiResourceList namedApiResourceList = await GetNamedApiResourceList("/api/v2/pokemon?limit=20");
            await listView.SetSourceAsync(namedApiResourceList.Results);
            return namedApiResourceList;
        }

        static async Task<NamedApiResourceList> GetNamedApiResourceList(string prefix)
        {
            return await ApiHelper.CallWebApiAsync(prefix);
        }

        static async Task<Pokemon> GetPokemon(string id)
        {
            return await ApiHelper.CallWebApiAsyncPokemon(id);
        }

        static async Task<PokemonSpecies> GetPokemonSpecies(string id)
        {
            return await ApiHelper.CallWebApiAsyncPokemonSpecies(id);
        }

        static async Task<EvolutionChain> GetPokemonEvolution(string id)
        {
            return await ApiHelper.CallWebApiAsyncEvolutionChain(id);
        }
    }
}
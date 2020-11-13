using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terminal.Gui;
using Pokedex.PokeApi.EndPoints;

namespace Pokedex
{
    class App : Window
    {
        private const string Left = "left";
        private const string Right = "right";
        private static readonly Uri BaseAddress = new Uri("https://pokeapi.co");

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

            var pokemonListView = new ListView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };

            var pokemonWindow = new Window("Pokemon")
            {
                X = Pos.Percent(50),
                Y = 6,
                Width = Dim.Percent(50),
                Height = Dim.Fill()
            };

            var pokemonNameTextView = new TextView()
            {
                ReadOnly = true,
                X = Pos.Center(),
                Y = 1,
                Height = 1
            };

            var pokemonTypeTextView = new TextView()
            {
                ReadOnly = true,
                X = Pos.Center(),
                Y = Pos.Bottom(pokemonNameTextView)+1,
                Height = 1
            };

            var pokemonDescriptionTextView = new TextView()
            {
                ReadOnly = true,
                X = Pos.Center(),
                Y = Pos.Bottom(pokemonTypeTextView)+1,
            };

            var pokemonEvolutionListView = new ListView()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(pokemonDescriptionTextView)+1,
            };

            var researchLabel = new Label("Recherche :")
            {
                X = 2,
                Y = 3
            };

            var researchTextField = new TextField("")
            {
                X = 2,
                Y = 4,
                Width = 25,
                Height = 1
            };

            var researchButton = new Button("Search") {X = Pos.Right(researchTextField) + 1, Y = 4};

            var previousButton = new Button("Previous") {X = 0, Y = Pos.AnchorEnd(1)};

            var nextButton = new Button("Next");
            nextButton.X = Pos.AnchorEnd() - (Pos.Right(nextButton) - Pos.Left(nextButton));
            nextButton.Y = Pos.AnchorEnd(1);

            pokemonListView.Initialized += (e, s) =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    namedApiResourceList = await SetPokemonListView(pokemonListView);
                });
            };

            void OnPokemonListViewOnOpenSelectedItem(ListViewItemEventArgs e)
            {
                NamedApiResource namedApiResource = (NamedApiResource) e.Value;

                Application.MainLoop.Invoke(async () =>
                {
                    Pokemon pokemon = await GetPokemon(new Uri(BaseAddress, $"api/v2/pokemon/{namedApiResource.Name}"));
                    List<LocationAreaEncounter> pokemonLocationAreaEncounters = await GetPokemonLocationAreaEncounters(pokemon.LocationAreaEncounters);
                    PokemonSpecies pokemonSpecies = await GetPokemonSpecies(pokemon.Species.Url);
                    EvolutionChain evolutionChain = await GetPokemonEvolution(pokemonSpecies.EvolutionChain.Url);

                    var organizedEvolutionChain = new List<NamedApiResource>();
                    ChainLink chainLink = evolutionChain.Chain;

                    do
                    {
                        organizedEvolutionChain.Add(chainLink.Species);
                        chainLink = chainLink.EvolvesTo[0];
                        if (chainLink.EvolvesTo.Count == 0)
                        {
                            organizedEvolutionChain.Add(chainLink.Species);
                        }
                    } while (chainLink.EvolvesTo.Count > 0);

                    await pokemonEvolutionListView.SetSourceAsync(organizedEvolutionChain);

                    string name = $"#{pokemon.Id} {pokemon.Name}";
                    string type = $"Type: {String.Join("/", pokemon.Types)}";
                    string description = pokemonSpecies.FlavorTextEntries.First(text => text.Language.Name == "fr").Text;

                    int descriptionHeight = description.Count(x => x == '\n')+1;
                    string[] descriptionSplit = description.Split('\n');
                    List<int> descriptionSplitLength = new List<int>();
                    foreach(var element in descriptionSplit)
                    {
                        descriptionSplitLength.Add(element.Length);
                    }

                    List<string> organizedEvolutionChainString = organizedEvolutionChain.ConvertAll(x => x.Name);
                    List<int>  evolutionLength = new List<int>();
                    foreach (var element in organizedEvolutionChainString)
                    {
                        evolutionLength.Add(element.Length);
                    }

                    pokemonNameTextView.Text = name;
                    pokemonNameTextView.Width = name.Length;

                    pokemonTypeTextView.Text = type;
                    pokemonTypeTextView.Width = type.Length;

                    pokemonDescriptionTextView.Text = description;
                    pokemonDescriptionTextView.Width = descriptionSplitLength.Max();
                    pokemonDescriptionTextView.Height = descriptionHeight;

                    pokemonEvolutionListView.Width = evolutionLength.Max();
                    pokemonEvolutionListView.Height = organizedEvolutionChain.Count;
                });
            }

            pokemonListView.OpenSelectedItem += OnPokemonListViewOnOpenSelectedItem;
            pokemonEvolutionListView.OpenSelectedItem += OnPokemonListViewOnOpenSelectedItem;

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

            pokemonWindow.Add(pokemonNameTextView, pokemonTypeTextView, pokemonDescriptionTextView, pokemonEvolutionListView);
            pokemonListWindow.Add(previousButton, nextButton, pokemonListView);
            top.Add(menu, win, pokemonListWindow, pokemonWindow, researchTextField, researchLabel, researchButton);
            Application.Run(top);
        }

        private static async Task<NamedApiResourceList> SetPokemonListView(NamedApiResourceList namedApiResourceList,
            ListView listView, string direction)
        {
            Uri uri = direction == Left ? namedApiResourceList.Previous : namedApiResourceList.Next;

            if (uri != null)
            {
                namedApiResourceList = await GetNamedApiResourceList(uri);
                await listView.SetSourceAsync(namedApiResourceList.Results);
            }

            return namedApiResourceList;
        }

        private static async Task<NamedApiResourceList> SetPokemonListView(ListView listView)
        {
            NamedApiResourceList namedApiResourceList =
                await GetNamedApiResourceList(new Uri(BaseAddress, $"api/v2/pokemon?limit=20"));
            await listView.SetSourceAsync(namedApiResourceList.Results);
            return namedApiResourceList;
        }

        static async Task<NamedApiResourceList> GetNamedApiResourceList(Uri uri)
        {
            return await ApiHelper<NamedApiResourceList>.GenericCallWebApiAsync(uri);
        }

        static async Task<Pokemon> GetPokemon(Uri uri)
        {
            return await ApiHelper<Pokemon>.GenericCallWebApiAsync(uri);
        }

        static async Task<List<LocationAreaEncounter>> GetPokemonLocationAreaEncounters(Uri uri)
        {
            return await ApiHelper<List<LocationAreaEncounter>>.GenericCallWebApiAsync(uri);
        }

        static async Task<PokemonSpecies> GetPokemonSpecies(Uri uri)
        {
            return await ApiHelper<PokemonSpecies>.GenericCallWebApiAsync(uri);
        }

        static async Task<EvolutionChain> GetPokemonEvolution(Uri uri)
        {
            return await ApiHelper<EvolutionChain>.GenericCallWebApiAsync(uri);
        }
    }
}
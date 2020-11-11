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

            var pokemonWindow = new Window("Pokemon")
            {
                X = Pos.Percent(50),
                Y = 6,
                Width = Dim.Percent(50),
                Height = Dim.Fill()
            };

            var pokemonNameLabel = new Label("")
            {
                X = 15,
                Y = 1
            };

            var pokemonSizeLabel = new Label("")
            {
                X = 15,
                Y = 3
            };

            var pokemonWeightLabel = new Label("")
            {
                X = 15,
                Y = 4
            };

            var pokemonDescriptionLabel = new Label("")
            {
                X = 12,
                Y = 6
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

            var pokemonEvolutionListView = new ListView()
            {
                X = 1,
                Y = 1,
                Width = 10,
                Height = 3
            };

            var pokemonAbilitiesListView = new ListView()
            {
                X = 1,
                Y = 5,
                Width = 10,
                Height = 3
            };

            var pokemonMovesListView = new ListView()
            {
                X = 1,
                Y = 10,
                Width = 10,
                Height = 5
            };

            var pokemonLocationAreaEncountersListView = new ListView()
            {
                X = 12,
                Y = 10,
                Width = 20,
                Height = 5
            };

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
                    await pokemonLocationAreaEncountersListView.SetSourceAsync(pokemonLocationAreaEncounters);
                    await pokemonAbilitiesListView.SetSourceAsync(pokemon.Abilities);
                    await pokemonMovesListView.SetSourceAsync(pokemon.Moves);

                    string nameLabel = $"#{pokemon.Id} {pokemon.Name} - {String.Join("/", pokemon.Types)}";
                    string description = pokemonSpecies.FlavorTextEntries.First(text => text.Language.Name == "fr").Text;
                    string height = $"Height: {pokemon.Height.ToString()}";
                    string weight = $"Weight: {pokemon.Weight.ToString()}";

                    pokemonNameLabel.Width = nameLabel.Length;
                    pokemonNameLabel.Text = nameLabel;

                    pokemonSizeLabel.Width = height.Length;
                    pokemonSizeLabel.Text = height;

                    pokemonWeightLabel.Width = weight.Length;
                    pokemonWeightLabel.Text = weight;

                    pokemonDescriptionLabel.Width = description.Length;
                    pokemonDescriptionLabel.Text = description;
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

            pokemonWindow.Add(pokemonNameLabel, pokemonDescriptionLabel, pokemonEvolutionListView,
                pokemonAbilitiesListView, pokemonMovesListView, pokemonLocationAreaEncountersListView, pokemonSizeLabel,
                pokemonWeightLabel);
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
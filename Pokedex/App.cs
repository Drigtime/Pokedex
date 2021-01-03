using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terminal.Gui;
using Pokedex.PokeApi.EndPoints;
using System.Runtime.Caching;
using System.Diagnostics;
using System.Collections;
using System.Net.Http;

namespace Pokedex
{
    class App : Window
    {
        static App()
        {
            HttpClient httpClient = new HttpClient();
            AppMethods appMethods = new AppMethods(httpClient);

            NamedApiResourceList namedApiResourceList = null;
            var cache = MemoryCache.Default;

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
                Y = Pos.Bottom(pokemonNameTextView) + 1,
                Height = 1
            };

            var pokemonDescriptionTextView = new TextView()
            {
                ReadOnly = true,
                X = Pos.Center(),
                Y = Pos.Bottom(pokemonTypeTextView) + 1,
            };

            var pokemonEvolutionListView = new ListView()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(pokemonDescriptionTextView) + 1,
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

            var researchButton = new Button("Search") { X = Pos.Right(researchTextField) + 1, Y = 4 };

            var previousButton = new Button("Previous") { X = 0, Y = Pos.AnchorEnd(1) };

            var nextButton = new Button("Next");
            nextButton.X = Pos.AnchorEnd() - (Pos.Right(nextButton) - Pos.Left(nextButton));
            nextButton.Y = Pos.AnchorEnd(1);

            pokemonListView.Initialized += (e, s) =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    //bool inCache = cache.Contains("PokemonList");


                    //enumerate cache's keys & values
                    AppMethods.GetCacheEnumAsync(cache);
                    var cacheLimit = cache.PhysicalMemoryLimit;

                    // cache test
                    if (cache.Contains("PokemonList"))
                    {
                        Debug.Write("\n\nla liste de pokemon est dans le cache\n\n");
                        Debug.Write("Pourcentage de cache max : " +cacheLimit);

                    }
                    else
                    {
                        Debug.Write("\n\nla liste de pokemon n'est pas dans le cache\n\n");
                        Debug.Write(cacheLimit);
                    }

                    NamedApiResourceList namedApiResourceListCacheIfNot = (NamedApiResourceList)MemoryCache.Default.AddOrGetExisting("PokemonList", await appMethods.SetPokemonListView(pokemonListView), DateTime.Now.AddHours(24));
                    namedApiResourceList = (NamedApiResourceList)MemoryCache.Default.Get("PokemonList");

                    if (cache.Contains("PokemonList"))
                    {
                        Debug.Write("\n\nla liste de pokemon est dans le cache\n\n");

                    }
                    else
                    {
                        Debug.Write("\n\nla liste de pokemon n'est pas dans le cache\n\n");
                    }
                });
            };

                void OnPokemonListViewOnOpenSelectedItem(ListViewItemEventArgs e)
                {
                    NamedApiResource namedApiResource = (NamedApiResource)e.Value;

                    Application.MainLoop.Invoke(async () =>
                    {
                        //enumerate cache's keys & values
                        AppMethods.GetCacheEnumAsync(cache);

                        Uri uri = new Uri(AppMethods.BaseAddress, $"api/v2/pokemon/{namedApiResource.Name}");

                        // cache test
                        if (cache.Contains($"{namedApiResource.Name}"))
                        {
                            Debug.WriteLine($"\n\n{namedApiResource.Name} is in the cache \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n\n");
                        }
                        else
                        {
                            Debug.WriteLine($"\n\n{namedApiResource.Name} is NOT CACHING\n Nombre d'elements dans la liste du cache : {cache.GetCount()}\n\n\n");
                        }


                        Pokemon pokemonCacheIfNot = (Pokemon)MemoryCache.Default.AddOrGetExisting($"{namedApiResource.Name}", await appMethods.GetPokemon(uri), DateTime.Now.AddHours(24));
                        Pokemon pokemon = (Pokemon)MemoryCache.Default.Get($"{namedApiResource.Name}");

                        List<LocationAreaEncounter> pokemonLocationAreaEncountersCacheIfNot = (List<LocationAreaEncounter>)MemoryCache.Default.AddOrGetExisting("PokemonLocationAreaEncounters" + namedApiResource.Name, await appMethods.GetPokemonLocationAreaEncounters(pokemon.LocationAreaEncounters), DateTime.Now.AddHours(24));
                        List<LocationAreaEncounter> pokemonLocationAreaEncounters = (List<LocationAreaEncounter>)MemoryCache.Default.Get("PokemonLocationAreaEncounters" + namedApiResource.Name);


                        PokemonSpecies pokemonSpeciesCacheIfNot = (PokemonSpecies)MemoryCache.Default.AddOrGetExisting("PokemonSpecies" + namedApiResource.Name, await appMethods.GetPokemonSpecies(pokemon.Species.Url), DateTime.Now.AddHours(24));
                        PokemonSpecies pokemonSpecies = (PokemonSpecies)MemoryCache.Default.Get("PokemonSpecies" + namedApiResource.Name);

                        EvolutionChain evolutionChainCacheIfNot = (EvolutionChain)MemoryCache.Default.AddOrGetExisting("EvolutionChain" + namedApiResource.Name, await appMethods.GetPokemonEvolution(pokemonSpecies.EvolutionChain.Url), DateTime.Now.AddHours(24));
                        EvolutionChain evolutionChain = (EvolutionChain)MemoryCache.Default.Get("EvolutionChain" + namedApiResource.Name);

                        var organizedEvolutionChain = new List<NamedApiResource>();
                        ChainLink chainLink = evolutionChain.Chain;

                        // cache test
                        if (cache.Contains($"{namedApiResource.Name}"))
                        {
                            Debug.WriteLine($"\n\n{namedApiResource.Name} is in the cache \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n");
                        }
                        else
                        {
                            Debug.WriteLine($"\n\n{namedApiResource.Name} is NOT CACHING \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n");
                        }

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

                        int descriptionHeight = description.Count(x => x == '\n') + 1;
                        string[] descriptionSplit = description.Split('\n');
                        List<int> descriptionSplitLength = new List<int>();
                        foreach (var element in descriptionSplit)
                        {
                            descriptionSplitLength.Add(element.Length);
                        }

                        List<string> organizedEvolutionChainString = organizedEvolutionChain.ConvertAll(x => x.Name);
                        List<int> evolutionLength = new List<int>();
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

                        //enumerate cache's keys & values
                        AppMethods.GetCacheEnumAsync(cache);
                    });
                }

                void OnPokemonListViewOnOpenSelectedItemResearchButton()
                {
                    //NamedApiResource namedApiResource = (NamedApiResource)e.Value;
                    string inputResearchText = Convert.ToString(researchTextField.Text);

                    Application.MainLoop.Invoke(async () =>
                    {
                        //enumerate cache's keys & values
                        AppMethods.GetCacheEnumAsync(cache);

                        Uri uri = new Uri(AppMethods.BaseAddress, $"api/v2/pokemon/{inputResearchText}");
                        Debug.Write(uri);
                        // cache test
                        if (cache.Contains($"{inputResearchText}"))
                        {
                            Debug.WriteLine($"\n\n{inputResearchText} is in the cache \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n\n");
                        }
                        else
                        {
                            Debug.WriteLine($"\n\n{inputResearchText} is NOT CACHING\n Nombre d'elements dans la liste du cache : {cache.GetCount()}\n\n\n");
                        }


                        Pokemon pokemonCacheIfNot = (Pokemon)MemoryCache.Default.AddOrGetExisting($"{inputResearchText}", await appMethods.GetPokemon(uri), DateTime.Now.AddHours(24));
                        Pokemon pokemon = (Pokemon)MemoryCache.Default.Get($"{inputResearchText}");

                        List<LocationAreaEncounter> pokemonLocationAreaEncounters = (List<LocationAreaEncounter>)MemoryCache.Default.AddOrGetExisting("PokemonLocationAreaEncounters" + inputResearchText, await appMethods.GetPokemonLocationAreaEncounters(pokemon.LocationAreaEncounters), DateTime.Now.AddHours(24));
                        List<LocationAreaEncounter> LocationAreaEncounters = (List<LocationAreaEncounter>)MemoryCache.Default.Get("PokemonLocationAreaEncounters" + inputResearchText);


                        PokemonSpecies pokemonSpeciesCacheIfNot = (PokemonSpecies)MemoryCache.Default.AddOrGetExisting("PokemonSpecies" + inputResearchText, await appMethods.GetPokemonSpecies(pokemon.Species.Url), DateTime.Now.AddHours(24));
                        PokemonSpecies pokemonSpecies = (PokemonSpecies)MemoryCache.Default.Get("PokemonSpecies" + inputResearchText);

                        EvolutionChain evolutionChainCacheIfNot = (EvolutionChain)MemoryCache.Default.AddOrGetExisting("EvolutionChain" + inputResearchText, await appMethods.GetPokemonEvolution(pokemonSpecies.EvolutionChain.Url), DateTime.Now.AddHours(24));
                        EvolutionChain evolutionChain = (EvolutionChain)MemoryCache.Default.Get("EvolutionChain" + inputResearchText);

                        var organizedEvolutionChain = new List<NamedApiResource>();
                        ChainLink chainLink = evolutionChain.Chain;

                        // cache test
                        if (cache.Contains($"{inputResearchText}"))
                        {
                            Debug.WriteLine($"\n\n{inputResearchText} is in the cache \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n");
                        }
                        else
                        {
                            Debug.WriteLine($"\n\n{inputResearchText} is NOT CACHING \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n");
                        }

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

                        int descriptionHeight = description.Count(x => x == '\n') + 1;
                        string[] descriptionSplit = description.Split('\n');
                        List<int> descriptionSplitLength = new List<int>();
                        foreach (var element in descriptionSplit)
                        {
                            descriptionSplitLength.Add(element.Length);
                        }

                        List<string> organizedEvolutionChainString = organizedEvolutionChain.ConvertAll(x => x.Name);
                        List<int> evolutionLength = new List<int>();
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

                        //enumerate cache's keys & values
                        AppMethods.GetCacheEnumAsync(cache);
                    });
                };


                pokemonListView.OpenSelectedItem += OnPokemonListViewOnOpenSelectedItem;
                pokemonEvolutionListView.OpenSelectedItem += OnPokemonListViewOnOpenSelectedItem;
                researchButton.Clicked += OnPokemonListViewOnOpenSelectedItemResearchButton;

                previousButton.Clicked += () =>
                {
                    Application.MainLoop.Invoke(async () =>
                    {
                        await appMethods.SetPokemonListView(namedApiResourceList, pokemonListView, AppMethods.Left);
                    });
                };

                nextButton.Clicked += () =>
                {
                    Application.MainLoop.Invoke(async () =>
                    {
                        await appMethods.SetPokemonListView(namedApiResourceList, pokemonListView, AppMethods.Right);
                    });
                };


                pokemonWindow.Add(pokemonNameTextView, pokemonTypeTextView, pokemonDescriptionTextView, pokemonEvolutionListView);
                pokemonListWindow.Add(previousButton, nextButton, pokemonListView);
                top.Add(menu, win, pokemonListWindow, pokemonWindow, researchTextField, researchLabel, researchButton);
                Application.Run(top);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.WebPages;
using PokeApi;
using Terminal.Gui;

namespace Pokedex
{
    internal class App : Window
    {
        static App()
        {
            var httpClient = new HttpClient();
            var appMethods = new AppMethods(httpClient);

            NamedApiResourceList namedApiResourceList = null;
            var cache = MemoryCache.Default;

            var paginationCounter = 0;

            Application.Init();

            var top = new Toplevel
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
                    })
                });

            var pokemonListWindow = new Window("List")
            {
                X = 0,
                Y = 6,
                Width = Dim.Percent(50),
                Height = Dim.Fill()
            };

            var pokemonListView = new ListView
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };

            var pokemonWindow = new Window("PokeApi")
            {
                X = Pos.Percent(50),
                Y = 6,
                Width = Dim.Percent(50),
                Height = Dim.Fill()
            };

            var pokemonNameTextView = new TextView
            {
                ReadOnly = true,
                X = Pos.Center(),
                Y = 1,
                Height = 1
            };

            var pokemonTypeTextView = new TextView
            {
                ReadOnly = true,
                X = Pos.Center(),
                Y = Pos.Bottom(pokemonNameTextView) + 1,
                Height = 1
            };

            var pokemonDescriptionTextView = new TextView
            {
                ReadOnly = true,
                X = Pos.Center(),
                Y = Pos.Bottom(pokemonTypeTextView) + 1
            };

            var pokemonEvolutionListView = new ListView
            {
                X = Pos.Center(),
                Y = Pos.Bottom(pokemonDescriptionTextView) + 1
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
                    //bool inCache = cache.Contains("PokemonList");


                    //enumerate cache's keys & values
                    AppMethods.GetCacheEnumAsync(cache);
                    var cacheLimit = cache.PhysicalMemoryLimit;

                    // cache test
                    if (cache.Contains("PokemonList"))
                    {
                        Debug.Write("\n\nla liste de pokemon est dans le cache\n\n");
                        Debug.Write("Pourcentage de cache max : " + cacheLimit);
                    }
                    else
                    {
                        Debug.Write("\n\nla liste de pokemon n'est pas dans le cache\n\n");
                        Debug.Write(cacheLimit);
                    }

                    if (!MemoryCache.Default.Contains($"PokemonList{paginationCounter}"))
                        MemoryCache.Default.Add($"PokemonList{paginationCounter}",
                            await AppMethods.GetPokemonList(), DateTime.Now.AddHours(24));

                    namedApiResourceList =
                        (NamedApiResourceList) MemoryCache.Default.Get($"PokemonList{paginationCounter}");

                    if (namedApiResourceList != null)
                        await pokemonListView.SetSourceAsync(namedApiResourceList.Results);

                    Debug.Write(cache.Contains("PokemonList")
                        ? "\n\nla liste de pokemon est dans le cache\n\n"
                        : "\n\nla liste de pokemon n'est pas dans le cache\n\n");
                });
            };

            void OnPokemonListViewOnOpenSelectedItem(ListViewItemEventArgs e)
            {
                var namedApiResource = (NamedApiResource) e.Value;

                Application.MainLoop.Invoke(async () =>
                {
                    //enumerate cache's keys & values
                    AppMethods.GetCacheEnumAsync(cache);

                    var uri = new Uri(AppMethods.BaseAddress, $"api/v2/pokemon/{namedApiResource.Name}");

                    // cache test
                    // Debug.WriteLine(
                    //     cache.Contains($"{namedApiResource.Name}")
                    //         ? $"\n\n{namedApiResource.Name} is in the cache \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n\n"
                    //         : $"\n\n{namedApiResource.Name} is NOT CACHING\n Nombre d'elements dans la liste du cache : {cache.GetCount()}\n\n\n");

                    if (!MemoryCache.Default.Contains($"{namedApiResource.Name}"))
                        MemoryCache.Default.Add($"{namedApiResource.Name}", await AppMethods.GetPokemon(uri),
                            DateTime.Now.AddHours(24));

                    var pokemon = (Pokemon) MemoryCache.Default.Get($"{namedApiResource.Name}");

                    if (pokemon != null)
                    {
                        if (!MemoryCache.Default.Contains($"PokemonLocationAreaEncounters{namedApiResource.Name}"))
                            MemoryCache.Default.Add($"PokemonLocationAreaEncounters{namedApiResource.Name}",
                                await AppMethods.GetPokemonLocationAreaEncounters(pokemon.LocationAreaEncounters),
                                DateTime.Now.AddHours(24));
                        MemoryCache.Default.Get($"PokemonLocationAreaEncounters{namedApiResource.Name}");
                        if (!MemoryCache.Default.Contains($"PokemonSpecies{namedApiResource.Name}"))
                            MemoryCache.Default.Add($"PokemonSpecies{namedApiResource.Name}",
                                await AppMethods.GetPokemonSpecies(pokemon.Species.Url), DateTime.Now.AddHours(24));
                        var pokemonSpecies =
                            (PokemonSpecies) MemoryCache.Default.Get($"PokemonSpecies{namedApiResource.Name}");

                        if (pokemonSpecies != null)
                        {
                            if (!MemoryCache.Default.Contains($"EvolutionChain{namedApiResource.Name}"))
                                MemoryCache.Default.Add($"EvolutionChain{namedApiResource.Name}",
                                    await AppMethods.GetPokemonEvolution(pokemonSpecies.EvolutionChain.Url),
                                    DateTime.Now.AddHours(24));
                            var evolutionChain =
                                (EvolutionChain) MemoryCache.Default.Get($"EvolutionChain{namedApiResource.Name}");

                            var organizedEvolutionChain = new List<NamedApiResource>();
                            if (evolutionChain != null)
                            {
                                var chainLink = evolutionChain.Chain;

                                // cache test
                                // Debug.WriteLine(
                                //    cache.Contains($"{namedApiResource.Name}")
                                //        ? $"\n\n{namedApiResource.Name} is in the cache \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n"
                                //        : $"\n\n{namedApiResource.Name} is NOT CACHING \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n");

                                do
                                {
                                    organizedEvolutionChain.Add(chainLink.Species);
                                    chainLink = chainLink.EvolvesTo[0];
                                    if (chainLink.EvolvesTo.Count == 0) organizedEvolutionChain.Add(chainLink.Species);
                                } while (chainLink.EvolvesTo.Count > 0);
                            }

                            await pokemonEvolutionListView.SetSourceAsync(organizedEvolutionChain);

                            var name = $"#{pokemon.Id} {pokemon.Name}";
                            var type = $"Type: {string.Join("/", pokemon.Types)}";
                            var description = pokemonSpecies.FlavorTextEntries.First(text => text.Language.Name == "fr")
                                .Text;

                            var descriptionHeight = description.Count(x => x == '\n') + 1;
                            var descriptionSplit = description.Split('\n');
                            var descriptionSplitLength = descriptionSplit.Select(element => element.Length).ToList();

                            var organizedEvolutionChainString = organizedEvolutionChain.ConvertAll(x => x.Name);
                            var evolutionLength = organizedEvolutionChainString.Select(element => element.Length)
                                .ToList();

                            pokemonNameTextView.Text = name;
                            pokemonNameTextView.Width = name.Length;

                            pokemonTypeTextView.Text = type;
                            pokemonTypeTextView.Width = type.Length;

                            pokemonDescriptionTextView.Text = description;
                            pokemonDescriptionTextView.Width = descriptionSplitLength.Max();
                            pokemonDescriptionTextView.Height = descriptionHeight;

                            pokemonEvolutionListView.Width = evolutionLength.Max();
                            pokemonEvolutionListView.Height = organizedEvolutionChain.Count;
                        }
                    }

                    //enumerate cache's keys & values
                    AppMethods.GetCacheEnumAsync(cache);
                });
            }

            void OnPokemonListViewOnOpenSelectedItemResearchButton()
            {
                //NamedApiResource namedApiResource = (NamedApiResource)e.Value;
                var inputResearchText = Convert.ToString(researchTextField.Text, CultureInfo.InvariantCulture);

                Application.MainLoop.Invoke(async () =>
                {
                    //enumerate cache's keys & values
                    AppMethods.GetCacheEnumAsync(cache);

                    var uri = new Uri(AppMethods.BaseAddress, $"api/v2/pokemon/{inputResearchText}");
                    // Debug.Write(uri);
                    // cache test
                    // Debug.WriteLine(
                    //     cache.Contains($"{inputResearchText}")
                    //         ? $"\n\n{inputResearchText} is in the cache \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n\n"
                    //         : $"\n\n{inputResearchText} is NOT CACHING\n Nombre d'elements dans la liste du cache : {cache.GetCount()}\n\n\n");

                    if (!inputResearchText.IsEmpty() && !MemoryCache.Default.Contains($"{inputResearchText}"))
                    {
                        var tmp = await AppMethods.GetPokemon(uri);
                        if (tmp != null)
                            MemoryCache.Default.Add($"{inputResearchText}", tmp, DateTime.Now.AddHours(24));
                    }

                    var pokemon = (Pokemon) MemoryCache.Default.Get($"{inputResearchText}");


                    if (pokemon != null)
                    {
                        if (!MemoryCache.Default.Contains($"PokemonLocationAreaEncounters{inputResearchText}"))
                            MemoryCache.Default.Add(
                                $"PokemonLocationAreaEncounters{inputResearchText}",
                                await AppMethods.GetPokemonLocationAreaEncounters(pokemon.LocationAreaEncounters),
                                DateTime.Now.AddHours(24));
                        MemoryCache.Default.Get($"PokemonLocationAreaEncounters{inputResearchText}");

                        if (!MemoryCache.Default.Contains($"PokemonSpecies{inputResearchText}"))
                            MemoryCache.Default.Add(
                                "PokemonSpecies" + inputResearchText,
                                await AppMethods.GetPokemonSpecies(pokemon.Species.Url),
                                DateTime.Now.AddHours(24));
                        var pokemonSpecies =
                            (PokemonSpecies) MemoryCache.Default.Get($"PokemonSpecies{inputResearchText}");

                        if (pokemonSpecies != null)
                        {
                            if (!MemoryCache.Default.Contains($"EvolutionChain{inputResearchText}"))
                                MemoryCache.Default.Add(
                                    "EvolutionChain" + inputResearchText,
                                    await AppMethods.GetPokemonEvolution(pokemonSpecies.EvolutionChain.Url),
                                    DateTime.Now.AddHours(24));
                            var evolutionChain =
                                (EvolutionChain) MemoryCache.Default.Get($"EvolutionChain{inputResearchText}");

                            var organizedEvolutionChain = new List<NamedApiResource>();
                            if (evolutionChain != null)
                            {
                                var chainLink = evolutionChain.Chain;

                                // cache test
                                // Debug.WriteLine(
                                //     cache.Contains($"{inputResearchText}")
                                //         ? $"\n\n{inputResearchText} is in the cache \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n"
                                //         : $"\n\n{inputResearchText} is NOT CACHING \nNombre d'elements dans la liste du cache : {cache.GetCount()}\n\n");

                                do
                                {
                                    organizedEvolutionChain.Add(chainLink.Species);
                                    chainLink = chainLink.EvolvesTo[0];
                                    if (chainLink.EvolvesTo.Count == 0) organizedEvolutionChain.Add(chainLink.Species);
                                } while (chainLink.EvolvesTo.Count > 0);
                            }

                            await pokemonEvolutionListView.SetSourceAsync(organizedEvolutionChain);

                            var name = $"#{pokemon.Id} {pokemon.Name}";
                            var type = $"Type: {string.Join("/", pokemon.Types)}";
                            var description = pokemonSpecies.FlavorTextEntries.First(text => text.Language.Name == "fr")
                                .Text;

                            var descriptionHeight = description.Count(x => x == '\n') + 1;
                            var descriptionSplit = description.Split('\n');
                            var descriptionSplitLength = descriptionSplit.Select(element => element.Length).ToList();

                            var organizedEvolutionChainString = organizedEvolutionChain.ConvertAll(x => x.Name);
                            var evolutionLength = organizedEvolutionChainString.Select(element => element.Length)
                                .ToList();

                            pokemonNameTextView.Text = name;
                            pokemonNameTextView.Width = name.Length;

                            pokemonTypeTextView.Text = type;
                            pokemonTypeTextView.Width = type.Length;

                            pokemonDescriptionTextView.Text = description;
                            pokemonDescriptionTextView.Width = descriptionSplitLength.Max();
                            pokemonDescriptionTextView.Height = descriptionHeight;

                            pokemonEvolutionListView.Width = evolutionLength.Max();
                            pokemonEvolutionListView.Height = organizedEvolutionChain.Count;
                        }
                    }
                    else
                    {
                        pokemonNameTextView.Text = "Aucun résultat";
                        pokemonNameTextView.Width = 14;
                        pokemonTypeTextView.Text = "";
                        pokemonTypeTextView.Width = 0;
                        pokemonDescriptionTextView.Text = "";
                        pokemonDescriptionTextView.Width = 0;
                        pokemonDescriptionTextView.Height = 0;
                        pokemonEvolutionListView.Width = 0;
                        pokemonEvolutionListView.Height = 0;
                    }

                    // enumerate cache's keys & values
                    AppMethods.GetCacheEnumAsync(cache);
                });
            }

            pokemonListView.OpenSelectedItem += OnPokemonListViewOnOpenSelectedItem;
            pokemonEvolutionListView.OpenSelectedItem += OnPokemonListViewOnOpenSelectedItem;
            researchButton.Clicked += OnPokemonListViewOnOpenSelectedItemResearchButton;

            previousButton.Clicked += () =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    paginationCounter = paginationCounter == 0 ? 0 : paginationCounter - 1;
                    namedApiResourceList =
                        await AppMethods.PaginatePokemonList(paginationCounter, namedApiResourceList, AppMethods.Left);
                    if (namedApiResourceList != null)
                        await pokemonListView.SetSourceAsync(namedApiResourceList.Results);
                });
            };

            nextButton.Clicked += () =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    paginationCounter += 1;
                    namedApiResourceList =
                        await AppMethods.PaginatePokemonList(paginationCounter, namedApiResourceList, AppMethods.Right);
                    if (namedApiResourceList != null)
                        await pokemonListView.SetSourceAsync(namedApiResourceList.Results);
                });
            };


            pokemonWindow.Add(pokemonNameTextView, pokemonTypeTextView, pokemonDescriptionTextView,
                pokemonEvolutionListView);
            pokemonListWindow.Add(previousButton, nextButton, pokemonListView);
            top.Add(menu, win, pokemonListWindow, pokemonWindow, researchTextField, researchLabel, researchButton);
            Application.Run(top);
        }
    }
}
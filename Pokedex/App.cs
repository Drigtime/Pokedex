using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using Pokedex.PokeApi;
using Pokedex.PokeApi.Objects;
using System.Collections;

namespace Pokedex
{
    class App : Window
    {
        private const string Left = "left";
        private const string Right = "right";
        private static readonly Uri uri = new Uri("https://pokeapi.co");

        static App()
        {
            NamedAPIResourceList resourceList = null;
            Pokemon pokemon = null;
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
                new MenuBarItem[]
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

            var researchButton = new Button("Rechercher");
            researchButton.X = Pos.Right(research) + 1;
            researchButton.Y = 4;

            var previousButton = new Button("Previous");
            previousButton.X = 0;
            previousButton.Y = Pos.AnchorEnd(1);

            var nextButton = new Button("Next");
            nextButton.X = Pos.AnchorEnd() - (Pos.Right(nextButton) - Pos.Left(nextButton));
            nextButton.Y = Pos.AnchorEnd(1);


            var listView = new ListView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };


            listView.Initialized += (e, s) =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    resourceList = await SetNamedApiResourceList(listView);
                });
            };

            listView.OpenSelectedItem += (e) =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    pokemon = await GetPokemon((NamedAPIResource)e.Value);
                    pokemonNameLabel.Width = pokemon.Name.Length;
                    pokemonNameLabel.Text = pokemon.Name;
                });
            };

            previousButton.Clicked += () =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    resourceList = await SetNamedApiResourceList(resourceList, listView, Left);
                });
            };

            nextButton.Clicked += () =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    resourceList = await SetNamedApiResourceList(resourceList, listView, Right);
                });
            };

            pokemonWindow.Add(pokemonNameLabel);
            pokemonListWindow.Add(previousButton, nextButton, listView);
            top.Add(menu, win, pokemonListWindow, pokemonWindow, research, researchLabel, researchButton);
            Application.Run(top);
        }

        private static async Task<NamedAPIResourceList> SetNamedApiResourceList(NamedAPIResourceList namedAPIResourceList, ListView listView, string direction)
        {
            Uri uri = direction == Left ? namedAPIResourceList.Previous : namedAPIResourceList.Next;

            if (uri != null)
            {
                namedAPIResourceList = await GetNamedApiResourceList(uri.PathAndQuery);
                listView.SetSource(namedAPIResourceList.Results);
            }

            return namedAPIResourceList;
        }

        private static async Task<NamedAPIResourceList> SetNamedApiResourceList(ListView listView)
        {
            NamedAPIResourceList namedAPIResourceList = await GetNamedApiResourceList();
            listView.SetSource(namedAPIResourceList.Results);
            return namedAPIResourceList;
        }

        static async Task<NamedAPIResourceList> GetNamedApiResourceList(string prefix = "/api/v2/pokemon?limit=20")
        {
            ApiHelper apiHelper = new ApiHelper(uri);
            NamedAPIResourceList resourceList = await apiHelper.CallWebAPIAsync(prefix);
            return resourceList;
        }

        static async Task<Pokemon> GetPokemon(NamedAPIResource namedAPIResource)
        {
            ApiHelper apiHelper = new ApiHelper(uri);
            Pokemon pokemon = await apiHelper.CallWebAPIAsyncPokemon(namedAPIResource.Url.PathAndQuery);
            return pokemon;
        }
    }
}
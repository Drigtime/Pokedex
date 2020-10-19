using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using Pokedex.PokeApi;

namespace Pokedex
{
    class App : Window
    {
        static App()
        {
            NamedAPIResourceList resourceList = null;

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
                new MenuBarItem[] {
                    //new MenuBarItem ("_File", new MenuItem [] {
                        new MenuBarItem ("_Quit", "", () => {
                            Application.Shutdown ();
                            Environment.Exit(0);
                        //})
                    }),
                });

            var list = new Window("List")
            {
                X = 0,
                Y = 6,
                Width = Dim.Percent(50),
                Height = Dim.Fill()
            };

            var specs = new Window("Specs")
            {
                X = Pos.Percent(50),
                Y = 6,
                Width = Dim.Percent(50),
                Height = Dim.Fill()
            };

            var researchLabel = new Label("Recherche :")
            {
                X = 2,
                Y = 3,
                Width = 25,
                Height = 1
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
            listView.OpenSelectedItem += (e) =>
            {
                Console.WriteLine(e.Value.ToString());
                //Application.MainLoop.Invoke(async () =>
                //{
                //    List<PokeApi.NamedAPIResource> pokemons = await Program.List();
                //    listView.SetSource(pokemons);
                //});
            };

            Application.MainLoop.Invoke(async () =>
            {
                resourceList = await Program.List();
                listView.SetSource(resourceList.Results.Select(pokemon => pokemon.Name).ToList());
            });

            previousButton.Clicked += () =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    if (resourceList.Previous != null)
                    {
                        resourceList = await Program.List(resourceList.Previous.Segments[3] + resourceList.Previous.Query);
                        listView.SetSource(resourceList.Results.Select(pokemon => pokemon.Name).ToList());
                    }
                });
            };
            nextButton.Clicked += () =>
            {
                Application.MainLoop.Invoke(async () =>
                {
                    if (resourceList.Next != null)
                    {
                        resourceList = await Program.List(resourceList.Next.Segments[3] + resourceList.Next.Query);
                        listView.SetSource(resourceList.Results.Select(pokemon => pokemon.Name).ToList());
                    }
                });
            };

            list.Add(previousButton, nextButton, listView);
            top.Add(menu, win, list, specs, research, researchLabel, researchButton);
            Application.Run(top);

        }
    }
}

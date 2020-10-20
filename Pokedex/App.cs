using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using Pokedex.PokeApi;

namespace Pokedex
{
    class App : Window
    {
        private const string Left = "left";
        private const string Right = "right";
 
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
            listView.Initialized += (e, s) => { resourceList = GetNamedApiResourceList(resourceList, listView); };
            listView.OpenSelectedItem += (e) =>
            {
                Console.WriteLine(e.Value);
            };

            previousButton.Clicked += () => { resourceList = GetNamedApiResourceList(resourceList, listView, Left); };
            nextButton.Clicked += () => { resourceList = GetNamedApiResourceList(resourceList, listView, Right); };

            list.Add(previousButton, nextButton, listView);
            top.Add(menu, win, list, specs, research, researchLabel, researchButton);
            Application.Run(top);

        }

        private static NamedAPIResourceList GetNamedApiResourceList(NamedAPIResourceList resourceList, ListView listView, string direction)
        {
            Application.MainLoop.Invoke(async () =>
            {
                Uri uri = direction == Left ? resourceList.Previous : resourceList.Next;
                
                if (uri != null)
                {
                    resourceList = await Program.List($"{uri.Segments[3]}{uri.Query}");
                    listView.SetSource(resourceList.Results);
                }
            });
            return resourceList;
        }
        
        private static NamedAPIResourceList GetNamedApiResourceList(NamedAPIResourceList resourceList, ListView listView)
        {
            Application.MainLoop.Invoke(async () =>
            {
                resourceList = await Program.List();
                listView.SetSource(resourceList.Results.Select(pokemon => pokemon.Name).ToList());
            });
            return resourceList;
        }
    }
}

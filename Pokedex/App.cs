using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Pokedex
{
    class App : Window
    {
        static App()
        {
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
                Width = 30,
                Height = 1
            };

            var research = new TextField("")
            {
                X = 2,
                Y = 4,
                Width = 30,
                Height = 1
            };

            var previousButton = new Button("Previous");
            previousButton.X = 0;
            previousButton.Y = Pos.AnchorEnd(1);

            var nextButton = new Button("Next");
            nextButton.X = Pos.AnchorEnd() - (Pos.Right(nextButton) - Pos.Left(nextButton));
            nextButton.Y = Pos.AnchorEnd(1);

            list.Add(previousButton, nextButton);
            top.Add(menu, win, list, specs, research, researchLabel);
            Application.Run(top);

        }
    }
}

﻿using MovieApp.DataAccess.Data.Entities;

namespace MovieApp.UI.Menu;

public class MainMenu : IMainMenu
{
    private readonly IMenu<Movie> _movieMenu;

    public MainMenu(IMenu<Movie> movieMenu)
    {
        _movieMenu = movieMenu;
    }

    public void LoadMenu()
    {
        Console.WriteLine("------- Welcome in Movie App -------\n");

        string choise;
        do
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Choose one option:");
            Console.WriteLine("\t1. Movies resources.");
            Console.WriteLine("\tQ. Quit.");
            Console.Write("\t\tYour choise: ");
            choise = Console.ReadLine()!.Trim().ToUpper();
            Console.WriteLine("-----------------------------------------------------------------------");

            switch (choise)
            {
                case "1":
                    _movieMenu.LoadMenu();
                    break;
                case "Q":
                    break;
                default:
                    Console.WriteLine("Choose one option or you stuck here forever!");
                    break;
            }

        } while (choise != "Q" );
    }
}
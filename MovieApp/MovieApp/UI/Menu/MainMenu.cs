using MovieApp.DataAccess.Data.Entities;
using MovieApp.UI.Menu.Extensions;

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
            MenuHelper.AddSeparator();
            Console.WriteLine("Choose one option:");
            Console.WriteLine("\t1. Movies resources.");
            Console.WriteLine("\tQ. Quit.");
            Console.Write("\t\tYour choise: ");
            choise = Console.ReadLine()!.Trim().ToUpper();
            MenuHelper.AddSeparator();

            switch (choise)
            {
                case "1":
                    _movieMenu.LoadMenu();
                    break;
                case "Q":
                    break;
                default:
                    Console.WriteLine("INFO : Choose one option or you stuck here forever!");
                    break;
            }

        } while (choise != "Q");
    }
}

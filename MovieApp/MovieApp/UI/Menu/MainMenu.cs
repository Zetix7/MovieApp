using MovieApp.DataAccess.Data.Entities;
using MovieApp.UI.Menu.Extensions;

namespace MovieApp.UI.Menu;

public class MainMenu : IMainMenu
{
    private readonly IMenu<Movie> _movieMenu;
    private readonly IMenu<Artist> _artistMenu;

    public MainMenu(IMenu<Movie> movieMenu, IMenu<Artist> artistMenu)
    {
        _movieMenu = movieMenu;
        _artistMenu = artistMenu;
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
            Console.WriteLine("\t2. Artists resources.");
            Console.WriteLine("\tQ. Quit.");
            Console.Write("\t\tYour choise: ");
            choise = Console.ReadLine()!.Trim().ToUpper();
            MenuHelper.AddSeparator();

            switch (choise)
            {
                case "1":
                    _movieMenu.LoadMenu();
                    break;
                case "2":
                    _artistMenu.LoadMenu();
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

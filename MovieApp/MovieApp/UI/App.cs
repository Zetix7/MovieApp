using MovieApp.UI.Menu;

namespace MovieApp.UI;

public class App : IApp
{
    private readonly IMainMenu _menu;

    public App(IMainMenu menu)
    {
        _menu = menu;
    }

    public void Run()
    {
        _menu.LoadMenu();
    }
}

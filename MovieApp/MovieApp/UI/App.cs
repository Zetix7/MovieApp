using MovieApp.AplicationServices.Components.FileCreator.CsvFile;
using MovieApp.AplicationServices.Components.FileCreator.XmlFile;
using MovieApp.UI.Menu;
using System.Xml;

namespace MovieApp.UI;

public class App : IApp
{
    private readonly IMenu _menu;

    public App(IMenu menu)
    {
        _menu = menu;
    }

    public void Run()
    {
        _menu.RunMenu();
    }
}

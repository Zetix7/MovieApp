using MovieApp.AplicationServices.Components.CsvFile;
using MovieApp.AplicationServices.Components.DataGenerator;
using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;

namespace MovieApp.UI;

public class App : IApp
{
    private readonly ICsvFileCreator _csvFileCreator;

    public App(ICsvFileCreator csvFileCreator)
    {
        _csvFileCreator = csvFileCreator;
    }

    public void Run()
    {
        _csvFileCreator.CreateMoviesCsvFileFromRepository();
    }
}

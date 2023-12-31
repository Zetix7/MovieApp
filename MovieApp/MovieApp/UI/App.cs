using MovieApp.AplicationServices.Components.FileCreator.CsvFile;

namespace MovieApp.UI;

public class App : IApp
{
    private readonly ICsvCreator _csvFileCreator;
    private readonly ICsvReader _csvReader;

    public App(ICsvCreator csvFileCreator, ICsvReader csvReader)
    {
        _csvFileCreator = csvFileCreator;
        _csvReader = csvReader;
    }

    public void Run()
    {
        _csvFileCreator.CreateMoviesCsvFileFromRepository();
        var movies = _csvReader.ReadMovieCsvFile();
    }
}

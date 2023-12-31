using MovieApp.AplicationServices.Components.FileCreator.CsvFile;
using MovieApp.AplicationServices.Components.FileCreator.XmlFile;
using System.Xml;

namespace MovieApp.UI;

public class App : IApp
{
    private readonly IXmlCreator _xmlCreator;
    private readonly ICsvCreator _csvCreator;
    private readonly IXmlReader _xmlReader;
    private readonly ICsvReader _csvReader;

    public App(IXmlCreator xmlCreator, ICsvCreator csvCreator, IXmlReader xmlReader, ICsvReader csvReader)
    {
        _csvCreator = csvCreator;
        _xmlCreator = xmlCreator;
        _xmlReader = xmlReader;
        _csvReader = csvReader;
    }

    public void Run()
    {
        _csvCreator.CreateMoviesCsvFileFromRepository();
        _csvReader.ReadMovieCsvFile();
        _xmlCreator.CreateMovieXmlFileFromRepository();
        _xmlReader.ReadMovieXmlFile();

        var movies = _xmlReader.ReadMovieXmlFile();

        foreach ( var movie in movies )
        {
            Console.WriteLine($"{movie.Title} ({movie.Year}) - {movie.Universe} - BoxOffice: {movie.BoxOffice}");
        }
    }
}

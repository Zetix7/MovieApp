using MovieApp.AplicationServices.Components.FileCreator.Models;
using System.Globalization;
using System.Xml.Linq;

namespace MovieApp.AplicationServices.Components.FileCreator.XmlFile;

public class XmlReader : IXmlReader
{
    public List<Movie> ReadMovieXmlFile()
    {
        if (!File.Exists($"DataAccess\\Resources\\Files\\movies.xml"))
        {
            throw new FileNotFoundException("Not found 'movies.csv' file!");
        }

        if(new FileInfo($"DataAccess\\Resources\\Files\\movies.xml").Length < 10)
        {
            throw new FileLoadException("File 'movies.csv' is empty!");
        }

        try
        {
            var xmlMovies = XDocument.Load($"DataAccess\\Resources\\Files\\movies.xml");
            var movies = xmlMovies.Element("Movies")!
                .Elements("Movie")!
                .Select(x => new Movie
                {
                    Title = x.Attribute("Title")!.Value,
                    Year = int.Parse(x.Attribute("Year")!.Value, CultureInfo.InvariantCulture),
                    Universe = x.Attribute("Universe")!.Value,
                    BoxOffice = decimal.Parse(x.Attribute("BoxOffice")!.Value, CultureInfo.InvariantCulture)
                }).ToList();
            return movies;
        }
        catch (Exception)
        {
            throw new Exception("File 'movies.csv' is broken!");
        }
    }
}

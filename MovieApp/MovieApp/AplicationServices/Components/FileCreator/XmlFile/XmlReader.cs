using MovieApp.AplicationServices.Components.FileCreator.Models;
using System.Globalization;
using System.Xml.Linq;

namespace MovieApp.AplicationServices.Components.FileCreator.XmlFile;

public class XmlReader : IXmlReader
{
    public List<Movie> ReadMovieXmlFile()
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
}

using MovieApp.AplicationServices.Components.FileCreator.Models;
using System.Globalization;
using System.Xml.Linq;

namespace MovieApp.AplicationServices.Components.FileCreator.XmlFile;

public class XmlReader : IXmlReader
{
    public event EventHandler<EventArgs?> MoviesXmlFileRead;
    public event EventHandler<EventArgs?> ArtistsXmlFileRead;

    public List<Artist> ReadArtistsXmlFile()
    {
        if (!File.Exists(@"DataAccess\Resources\Files\artists.xml"))
        {
            throw new FileNotFoundException("ERROR : Not found 'artists.xml' file!");
        }

        if(new FileInfo(@"DataAccess\Resources\Files\artists.xml").Length < 10)
        {
            throw new FileLoadException("ERROR : File 'artists.xml' is empty!");
        }

        var document = XDocument.Load(@"DataAccess\Resources\Files\artists.xml");

        try
        {
            var artists = document.Element("Artists")!
            .Elements("Artist")!
            .Select(x => new Artist
            {
                FirstName = x.Attribute("FirstName")!.Value,
                LastName = x.Attribute("LastName")!.Value
            }).ToList();

            ArtistsXmlFileRead?.Invoke(this, new EventArgs());

            return artists;
        }
        catch (Exception)
        {
            throw new Exception("ERROR : File 'artists.xml' is broken!");
        }
    }

    public List<Movie> ReadMoviesXmlFile()
    {
        if (!File.Exists(@"DataAccess\Resources\Files\movies.xml"))
        {
            throw new FileNotFoundException("ERROR : Not found 'movies.csv' file!");
        }

        if(new FileInfo(@"DataAccess\Resources\Files\movies.xml").Length < 10)
        {
            throw new FileLoadException("ERROR : File 'movies.csv' is empty!");
        }

        try
        {
            var xmlMovies = XDocument.Load(@"DataAccess\Resources\Files\movies.xml");
            var movies = xmlMovies.Element("Movies")!
                .Elements("Movie")!
                .Select(x => new Movie
                {
                    Title = x.Attribute("Title")!.Value,
                    Year = int.Parse(x.Attribute("Year")!.Value, CultureInfo.InvariantCulture),
                    Universe = x.Attribute("Universe")!.Value,
                    BoxOffice = decimal.Parse(x.Attribute("BoxOffice")!.Value, CultureInfo.InvariantCulture)
                }).ToList();

            MoviesXmlFileRead?.Invoke(this, new EventArgs());

            return movies;
        }
        catch (Exception)
        {
            throw new Exception("ERROR : File 'movies.csv' is broken!");
        }
    }
}

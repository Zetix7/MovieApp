using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;
using System.Xml.Linq;

namespace MovieApp.AplicationServices.Components.FileCreator.XmlFile;

public class XmlCreator : IXmlCreator
{
    private readonly IRepository<Movie> _movieRepository;
    private readonly IRepository<Artist> _artistRepository;

    public XmlCreator(IRepository<Movie> movieRepository, IRepository<Artist> artistRepository)
    {
        _movieRepository = movieRepository;
        _artistRepository = artistRepository;
    }

    public void CreateArtistsXmlFileFromRepository()
    {
        var artists = _artistRepository.GetAll();

        if (!artists.Any())
        {
            throw new ArgumentException("ERROR : Repository is empty!");
        }

        var items = new XElement("Artists", artists.Select(x =>
            new XElement("Artist", 
                new XAttribute("FirstName", x.FirstName!),
                new XAttribute("LastName", x.LastName!))));

        var document = new XDocument(items);
        document.Save(@"DataAccess\Resources\Files\artists.xml");
    }

    public void CreateMoviesXmlFileFromRepository()
    {
        var movies = _movieRepository.GetAll();

        if (!movies.Any())
        {
            throw new ArgumentException("ERROR :Repository is empty!");
        }

        var items = new XElement("Movies", movies.Select(x =>
            new XElement("Movie",
                new XAttribute("Title", x.Title!),
                new XAttribute("Year", x.Year),
                new XAttribute("Universe", x.Universe!),
                new XAttribute("BoxOffice", x.BoxOffice))));

        var document = new XDocument(items);
        document.Save(@"DataAccess\Resources\Files\movies.xml");
    }
}

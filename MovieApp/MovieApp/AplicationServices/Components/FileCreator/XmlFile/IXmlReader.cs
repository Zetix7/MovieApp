using MovieApp.AplicationServices.Components.FileCreator.Models;

namespace MovieApp.AplicationServices.Components.FileCreator.XmlFile;

public interface IXmlReader
{
    event EventHandler<EventArgs?> MoviesXmlFileRead;

    List<Movie> ReadMoviesXmlFile();
    List<Artist> ReadArtistsXmlFile();
}

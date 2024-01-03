using MovieApp.AplicationServices.Components.FileCreator.Models;

namespace MovieApp.AplicationServices.Components.FileCreator.XmlFile;

public interface IXmlReader
{
    List<Movie> ReadMoviesXmlFile();
    List<Artist> ReadArtistsXmlFile();
}

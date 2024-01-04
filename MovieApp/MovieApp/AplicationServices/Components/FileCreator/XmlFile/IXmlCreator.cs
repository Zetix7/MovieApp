namespace MovieApp.AplicationServices.Components.FileCreator.XmlFile;

public interface IXmlCreator
{
    event EventHandler<EventArgs?> MoviesXmlFileCreated;
    event EventHandler<EventArgs?> ArtistsXmlFileCreated;

    void CreateMoviesXmlFileFromRepository();
    void CreateArtistsXmlFileFromRepository();
}

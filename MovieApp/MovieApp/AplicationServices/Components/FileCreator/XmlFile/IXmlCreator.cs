namespace MovieApp.AplicationServices.Components.FileCreator.XmlFile;

public interface IXmlCreator
{
    event EventHandler<EventArgs?> MoviesXmlFileCreated;

    void CreateMoviesXmlFileFromRepository();
    void CreateArtistsXmlFileFromRepository();
}

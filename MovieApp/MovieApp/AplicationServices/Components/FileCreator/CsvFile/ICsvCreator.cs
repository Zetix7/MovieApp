namespace MovieApp.AplicationServices.Components.FileCreator.CsvFile;

public interface ICsvCreator
{
    event EventHandler<EventArgs?> MoviesCsvFileCreated;
    event EventHandler<EventArgs?> ArtistsCsvFileCreated;

    void CreateMoviesCsvFileFromRepository();
    void CreateArtistsCsvFileFromRepository();
}

namespace MovieApp.AplicationServices.Components.FileCreator.CsvFile;

public interface ICsvCreator
{
    event EventHandler<EventArgs?> MoviesCsvFileCreated;

    void CreateMoviesCsvFileFromRepository();
    void CreateArtistsCsvFileFromRepository();
}

using MovieApp.AplicationServices.Components.FileCreator.Models;

namespace MovieApp.AplicationServices.Components.FileCreator.CsvFile;

public interface ICsvReader
{
    event EventHandler<EventArgs?> ReadMoviesCsvFileEvent;
    event EventHandler<EventArgs?> ReadArtistsCsvFileEvent;

    List<Movie> ReadMoviesCsvFile();
    List<Artist> ReadArtistsCsvFile();
}

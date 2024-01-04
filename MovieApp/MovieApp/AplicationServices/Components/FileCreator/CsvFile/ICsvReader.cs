using MovieApp.AplicationServices.Components.FileCreator.Models;

namespace MovieApp.AplicationServices.Components.FileCreator.CsvFile;

public interface ICsvReader
{
    event EventHandler<EventArgs?> ReadMoviesCsvFileEvent;

    List<Movie> ReadMoviesCsvFile();
    List<Artist> ReadArtistsCsvFile();
}

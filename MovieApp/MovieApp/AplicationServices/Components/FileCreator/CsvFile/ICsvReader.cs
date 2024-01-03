using MovieApp.AplicationServices.Components.FileCreator.Models;

namespace MovieApp.AplicationServices.Components.FileCreator.CsvFile;

public interface ICsvReader
{
    List<Movie> ReadMoviesCsvFile();
    List<Artist> ReadArtistsCsvFile();
}

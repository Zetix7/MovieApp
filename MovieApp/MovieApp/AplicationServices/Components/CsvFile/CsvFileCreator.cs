using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;

namespace MovieApp.AplicationServices.Components.CsvFile;

public class CsvFileCreator : ICsvFileCreator
{
    private readonly IRepository<Movie> _movieRepository;

    public CsvFileCreator(IRepository<Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public void CreateMoviesCsvFileFromRepository()
    {
        var movies = _movieRepository.GetAll();

        using (var writer = File.CreateText(@"DataAccess\Resources\Files\movies.csv"))
        {
            foreach(var movie in movies)
            {
                writer.WriteLine($"{movie.Title},{movie.Year},{movie.Universe},{movie.BoxOffice}");
            }
        }
    }
}

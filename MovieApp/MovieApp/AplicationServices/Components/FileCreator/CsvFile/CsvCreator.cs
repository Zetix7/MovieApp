using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;

namespace MovieApp.AplicationServices.Components.FileCreator.CsvFile;

public class CsvCreator : ICsvCreator
{
    private readonly IRepository<Movie> _movieRepository;

    public CsvCreator(IRepository<Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public void CreateMoviesCsvFileFromRepository()
    {
        var movies = _movieRepository.GetAll();

        using (var writer = File.CreateText(@"DataAccess\Resources\Files\movies.csv"))
        {
            foreach (var movie in movies)
            {
                writer.WriteLine($"{movie.Title},{movie.Year},{movie.Universe},{movie.BoxOffice}");
            }
        }
    }
}

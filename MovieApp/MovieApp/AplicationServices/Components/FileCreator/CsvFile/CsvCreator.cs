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

        if (!movies.Any())
        {
            throw new ArgumentException("Repository is empty!");
        }

        using (var writer = File.CreateText(@"DataAccess\Resources\Files\movies.csv"))
        {
            foreach (var movie in movies)
            {
                var newBoxOffice = ConvertBoxOfficeValue(movie);
                writer.WriteLine($"{movie.Title},{movie.Year},{movie.Universe},{newBoxOffice}");
            }
        }
    }

    private static string ConvertBoxOfficeValue(Movie movie)
    {
        var splitBoxOffice = movie.BoxOffice.ToString().Split(",");
        return $"{splitBoxOffice[0]}.{splitBoxOffice[1]}";
    }
}

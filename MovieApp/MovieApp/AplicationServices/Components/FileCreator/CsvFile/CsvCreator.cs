using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;

namespace MovieApp.AplicationServices.Components.FileCreator.CsvFile;

public class CsvCreator : ICsvCreator
{
    private readonly IRepository<Movie> _movieRepository;
    private readonly IRepository<Artist> _artistRepository;

    public CsvCreator(IRepository<Movie> movieRepository, IRepository<Artist> artistRepository)
    {
        _movieRepository = movieRepository;
        _artistRepository = artistRepository;
    }

    public event EventHandler<EventArgs?> MoviesCsvFileCreated;

    public void CreateArtistsCsvFileFromRepository()
    {
        var artists = _artistRepository.GetAll();

        if(!artists.Any())
        {
            throw new ArgumentException("ERROR : Repository is empty!");
        }

        using(var writer = File.CreateText(@"DataAccess\Resources\Files\artists.csv"))
        {
            foreach(var artist in artists)
            {
                writer.WriteLine($"{artist.FirstName},{artist.LastName}");
            }
        }
    }

    public void CreateMoviesCsvFileFromRepository()
    {
        var movies = _movieRepository.GetAll();

        if (!movies.Any())
        {
            throw new ArgumentException("ERROR : Repository is empty!");
        }

        using (var writer = File.CreateText(@"DataAccess\Resources\Files\movies.csv"))
        {
            foreach (var movie in movies)
            {
                var newBoxOffice = ConvertBoxOfficeValue(movie);
                writer.WriteLine($"{movie.Title},{movie.Year},{movie.Universe},{newBoxOffice}");
            }
        }

        MoviesCsvFileCreated?.Invoke(this, new EventArgs());
    }

    private static string ConvertBoxOfficeValue(Movie movie)
    {
        var splitBoxOffice = movie.BoxOffice.ToString().Split(",");
        return $"{splitBoxOffice[0]}.{splitBoxOffice[1]}";
    }
}

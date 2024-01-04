using MovieApp.AplicationServices.Components.FileCreator.Models;
using System.Globalization;

namespace MovieApp.AplicationServices.Components.FileCreator.CsvFile;

public class CsvReader : ICsvReader
{
    public event EventHandler<EventArgs?> ReadMoviesCsvFileEvent;

    public List<Movie> ReadMoviesCsvFile()
    {
        if (!File.Exists(@"DataAccess\Resources\Files\movies.csv"))
        {
            throw new FileNotFoundException("ERROR : Not found 'movies.csv' file!");
        }

        if(new FileInfo(@"DataAccess\Resources\Files\movies.csv").Length < 10)
        {
            throw new FileLoadException("ERROR : File 'movies.csv' is empty!");
        }

        try
        {
            var movies = File.ReadAllLines(@"DataAccess\Resources\Files\movies.csv").Where(x => x.Length > 0).Select(x =>
            {
                var movie = x.Split(',');
                return new Movie
                {
                    Title = movie[0],
                    Year = int.Parse(movie[1], CultureInfo.InvariantCulture),
                    Universe = movie[2],
                    BoxOffice = decimal.Parse(movie[3], CultureInfo.InvariantCulture),
                };
            }).ToList();

            ReadMoviesCsvFileEvent?.Invoke(this, new EventArgs());

            return movies;
        }
        catch (Exception)
        {
            throw new Exception("ERROR : File 'movies.csv' is broken!");
        }
    }

    public List<Artist> ReadArtistsCsvFile()
    {
        if (!File.Exists(@"DataAccess\Resources\Files\artists.csv"))
        {
            throw new FileNotFoundException("ERROR : Not found 'artists.csv' file!");
        }

        if (new FileInfo(@"DataAccess\Resources\Files\artists.csv").Length < 10)
        {
            throw new FileLoadException("ERROR : File 'artists.csv' is empty!");
        }

        try
        {
            var artists = File.ReadAllLines(@"DataAccess\Resources\Files\artists.csv").Where(x => x.Length > 0).Select(x =>
            {
                var artist = x.Split(',');
                return new Artist
                {
                    FirstName = artist[0],
                    LastName = artist[1],
                };
            }).ToList();

            return artists;
        }
        catch (Exception)
        {
            throw new Exception("ERROR : File 'artists.csv' is broken!");
        }
    }
}

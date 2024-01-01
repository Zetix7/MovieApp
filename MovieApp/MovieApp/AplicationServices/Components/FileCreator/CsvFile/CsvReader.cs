using MovieApp.AplicationServices.Components.FileCreator.Models;
using System.Globalization;

namespace MovieApp.AplicationServices.Components.FileCreator.CsvFile;

public class CsvReader : ICsvReader
{
    public List<Movie> ReadMovieCsvFile()
    {
        if (!File.Exists(@"DataAccess\Resources\Files\movies.csv"))
        {
            throw new FileNotFoundException("Not found 'movies.csv' file!");
        }

        if(new FileInfo(@"DataAccess\Resources\Files\movies.csv").Length < 10)
        {
            throw new FileLoadException("File 'movies.csv' is empty!");
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

            return movies;
        }
        catch (Exception)
        {
            throw new Exception("File 'movies.csv' is broken!");
        }
    }
}

using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;
using System.Globalization;

namespace MovieApp.AplicationServices.Components.FileCreator.CsvFile;

public class CsvReader : ICsvReader
{
    public List<Models.Movie> ReadMovieCsvFile()
    {
        var movies = File.ReadAllLines(@"DataAccess\Resources\Files\movies.csv").Where(x => x.Length > 0).Select(x => 
        {
            var movie = x.Split(',');
            return new Models.Movie
            {
                Title = movie[0],
                Year = int.Parse(movie[1], CultureInfo.InvariantCulture),
                Universe = movie[2],
                BoxOffice = decimal.Parse(movie[3], CultureInfo.InvariantCulture),
            };
        }).ToList();

        return movies;
    }
}

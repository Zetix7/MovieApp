using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;

var movies = new ListRepository<Movie>();
movies.Add(new Movie { Id = 1, Title = "Avengers", Year = 2012, Universe = "Marvel", BoxOffice = 1_521_000_000.00m });
movies.Add(new Movie { Id = 2, Title = "Avengers: Age of Ultron", Year = 2015, Universe = "Marvel", BoxOffice = 1_409_000_000.00m });

foreach(var movie in movies.GetAll())
{
    Console.WriteLine(movie);
}
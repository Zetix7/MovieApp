using MovieApp.AplicationServices.Components.DataGenerator;
using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;

namespace MovieApp.UI;

public class App : IApp
{
    private readonly IDataGenerator _dataGenerator;

    public App(IDataGenerator dataGenerator)
    {
        _dataGenerator = dataGenerator;
    }

    public void Run()
    {
        var movies = new ListRepository<Movie>();
        foreach (var movie in _dataGenerator.GenerateSampleMovies())
        {
            movies.Add(movie);
        }

        foreach (var movie in movies.GetAll())
        {
            Console.WriteLine(movie);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MovieApp.AplicationServices.Components.DataGenerator;
using MovieApp.DataAccess;
using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;

namespace MovieApp.UI;

public class App : IApp
{
    private readonly IDataGenerator _dataGenerator;
    private readonly MovieAppDbContext _dbContext;
    private readonly IRepository<Movie> _movieRepository;

    public App(IDataGenerator dataGenerator,MovieAppDbContext dbContext, IRepository<Movie> movieRepository)
    {
        _dataGenerator = dataGenerator;
        _dbContext = dbContext;
        _movieRepository = movieRepository;
    }

    public void Run()
    {
        var movies = new SqlRepository<Movie>(_dbContext);
        foreach (var movie in _dataGenerator.GenerateSampleMovies())
        {
            movies.Add(movie);
            movies.Save();
        }

        foreach (var movie in movies.GetAll())
        {
            Console.WriteLine(movie);
        }
    }
}

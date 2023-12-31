using MovieApp.DataAccess.Data.Entities;

namespace MovieApp.AplicationServices.Components.DataGenerator;

public interface IDataGenerator
{
    List<Movie> GenerateSampleMovies();
}

using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;

namespace MovieApp.UI.Menu;

public class MoviesMenu : Menu<Movie>
{
    private readonly IRepository<Movie> _movieRepository;

    public MoviesMenu(IRepository<Movie> movieRepository) : base(movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public override void LoadMenu()
    {
        Console.WriteLine($"\n------- Movies Menu -------\n");

        string choise;
        do
        {
            base.LoadMenu();
            choise = Console.ReadLine()!.Trim().ToUpper();

            switch (choise)
            {
                case "1":
                    PrintAllItems();
                    break;
                case "2":
                    PrintItemById();
                    break;
                case "3":
                    AddNewItemToRepository();
                    break;
                case "4":
                    RemoveItemToRepository();
                    break;
                case "5":
                    break;
                case "6":
                    break;
                case "7":
                    break;
                case "8":
                    break;
                case "Q":
                    break;
                default:
                    Console.WriteLine("Choose one option or you stuck here forever!");
                    break;
            }

        } while (choise != "Q");
    }

    protected override void RemoveItemToRepository()
    {
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.Write("\tInsert movie Id to remove from repository: ");
        var id = Console.ReadLine()!.Trim();

        if (!int.TryParse(id, out var newId))
        {
            throw new FormatException($"Invalid Id '{id}'! This is not integer!");
        }

        if (!_movieRepository.GetAll().Where(x => x.Id == newId).Any())
        {
            throw new ArgumentException("Id not exist in repository!");
        }

        var movie = _movieRepository.GetAll().SingleOrDefault(x=>x.Id== newId);

        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine("\tAre you sure to remove this movie?:");
        Console.WriteLine(movie);

        Console.Write("\t\tYour choise (Y/N): ");
        var choise = Console.ReadLine()!.Trim().ToUpper();
        Console.WriteLine("-----------------------------------------------------------------------");
        if (choise.Equals("Y"))
        {
            _movieRepository.Remove(movie!);
            _movieRepository.Save();
            Console.WriteLine("INFO : Movie removed successfully.");
        }
        else
        {
            Console.WriteLine("INFO : Remove aborted.");
        }
    }

    protected override void AddNewItemToRepository()
    {
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.Write("\tInsert title: ");
        var title = Console.ReadLine()!.Trim();

        Console.Write("\tInsert year: ");
        var year = Console.ReadLine()!.Trim();

        if (!int.TryParse(year, out var newYear))
        {
            throw new FormatException($"Invalid year '{year}'! This is not integer!");
        }

        Console.Write("\tInsert universe: ");
        var universe = Console.ReadLine()!.Trim();

        Console.Write("\tInsert box office: ");
        var boxOffice = Console.ReadLine()!.Trim();

        if (!decimal.TryParse(boxOffice, out var newBoxOffice))
        {
            throw new FormatException($"Invalid box office '{boxOffice}'! This is not price!");
        }

        if (_movieRepository.GetAll().Where(x => x.Title == title && x.Year == newYear).Any())
        {
            throw new ArgumentException("Movie exist in repository!");
        }

        _movieRepository.Add(new Movie { Title = title, Year = newYear, Universe = universe, BoxOffice = newBoxOffice });
        _movieRepository.Save();

        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine($"INFO : New movie added to repository.\n\n{_movieRepository.GetAll().LastOrDefault(x=>x.Title == title)}");
    }
}

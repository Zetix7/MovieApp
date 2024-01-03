using MovieApp.AplicationServices.Components.FileCreator.CsvFile;
using MovieApp.AplicationServices.Components.FileCreator.XmlFile;
using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;
using MovieApp.UI.Menu.Extensions;

namespace MovieApp.UI.Menu;

public class MoviesMenu : Menu<Movie>
{
    private readonly IRepository<Movie> _movieRepository;
    private readonly ICsvCreator _csvCreator;
    private readonly ICsvReader _csvReader;
    private readonly IXmlCreator _xmlCreator;
    private readonly IXmlReader _xmlReader;

    public MoviesMenu(IRepository<Movie> movieRepository,
        ICsvCreator csvCreator,
        ICsvReader csvReader,
        IXmlCreator xmlCreator,
        IXmlReader xmlReader) : base(movieRepository)
    {
        _movieRepository = movieRepository;
        _csvCreator = csvCreator;
        _csvReader = csvReader;
        _xmlCreator = xmlCreator;
        _xmlReader = xmlReader;
    }

    public override void LoadMenu()
    {
        Console.WriteLine($"\n------- Movies Menu -------\n");

        string choise;
        do
        {
            base.LoadMenu();
            choise = Console.ReadLine()!.Trim().ToUpper();

            try
            {
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
                        RemoveItemFromRepository();
                        break;
                    case "5":
                        CreateCsvFile();
                        break;
                    case "6":
                        ReadCsvFile();
                        break;
                    case "7":
                        CreateXmlFile();
                        break;
                    case "8":
                        ReadXmlFile();
                        break;
                    case "Q":
                        break;
                    default:
                        MenuHelper.AddSeparator();
                        Console.WriteLine("INFO : Choose one option or you stuck here forever!");
                        break;
                }
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message);
            }
            catch (FileNotFoundException fe)
            {
                Console.WriteLine(fe.Message);
            }
            catch (FileLoadException fe)
            {
                Console.WriteLine(fe.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } while (choise != "Q");
    }
    protected override void ReadXmlFile()
    {
        var movies = _xmlReader.ReadMovieXmlFile();
        MenuHelper.AddSeparator();
        foreach (var movie in movies)
        {
            Console.WriteLine(movie);
        }
    }

    protected override void CreateXmlFile()
    {
        _xmlCreator.CreateMovieXmlFileFromRepository();
        MenuHelper.AddSeparator();
        Console.WriteLine("INFO : Created movies.xml file.");
    }

    protected override void ReadCsvFile()
    {
        var movies = _csvReader.ReadMovieCsvFile();
        MenuHelper.AddSeparator();
        foreach (var movie in movies)
        {
            Console.WriteLine(movie);
        }
    }

    protected override void CreateCsvFile()
    {
        _csvCreator.CreateMoviesCsvFileFromRepository();
        MenuHelper.AddSeparator();
        Console.WriteLine("INFO : Created movies.csv file.");
    }

    protected override void RemoveItemFromRepository()
    {
        MenuHelper.AddSeparator();
        Console.Write("\tInsert movie Id to remove from repository: ");
        var id = Console.ReadLine()!.Trim();

        if (!int.TryParse(id, out var newId))
        {
            MenuHelper.AddSeparator();
            throw new FormatException($"ERROR : Invalid Id '{id}'! This is not integer!");
        }

        if (!_movieRepository.GetAll().Where(x => x.Id == newId).Any())
        {
            MenuHelper.AddSeparator();
            throw new ArgumentException("ERROR : Id not exists in repository!");
        }

        var movie = _movieRepository.GetAll().SingleOrDefault(x => x.Id == newId);

        MenuHelper.AddSeparator();
        Console.WriteLine("\tAre you sure to remove this movie?:");
        Console.WriteLine(movie);

        Console.Write("\t\tYour choise (Y/N): ");
        var choise = Console.ReadLine()!.Trim().ToUpper();
        MenuHelper.AddSeparator();
        if (choise.Equals("Y"))
        {
            _movieRepository.Remove(movie!);
            _movieRepository.Save();
            Console.WriteLine("INFO : Movie removed successfully.");
        }
        else
        {
            Console.WriteLine("INFO : Movie remove aborted.");
        }
    }

    protected override void AddNewItemToRepository()
    {
        MenuHelper.AddSeparator();
        Console.Write("\tInsert title: ");
        var title = Console.ReadLine()!.Trim();

        Console.Write("\tInsert year: ");
        var year = Console.ReadLine()!.Trim();

        if (!int.TryParse(year, out var newYear))
        {
            MenuHelper.AddSeparator();
            throw new FormatException($"ERROR : Invalid year '{year}'! This is not integer!");
        }

        Console.Write("\tInsert universe: ");
        var universe = Console.ReadLine()!.Trim();

        Console.Write("\tInsert box office: ");
        var boxOffice = Console.ReadLine()!.Trim();

        if (!decimal.TryParse(boxOffice, out var newBoxOffice))
        {
            MenuHelper.AddSeparator();
            throw new FormatException($"ERROR : Invalid box office '{boxOffice}'! This is not price!");
        }

        if (_movieRepository.GetAll().Where(x => x.Title == title && x.Year == newYear).Any())
        {
            MenuHelper.AddSeparator();
            throw new ArgumentException("ERROR : Movie exist in repository!");
        }

        _movieRepository.Add(new Movie { Title = title, Year = newYear, Universe = universe, BoxOffice = newBoxOffice });
        _movieRepository.Save();

        MenuHelper.AddSeparator();
        Console.WriteLine($"INFO : New movie added to repository.\n\n{_movieRepository.GetAll().LastOrDefault(x => x.Title == title)}");
    }
}

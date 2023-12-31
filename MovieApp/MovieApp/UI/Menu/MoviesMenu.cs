﻿using MovieApp.AplicationServices.Components.DataGenerator;
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
    private readonly IDataGenerator _dataGenerator;
    private const string FILENAME = "movies";

    public MoviesMenu(IRepository<Movie> movieRepository,
        ICsvCreator csvCreator,
        ICsvReader csvReader,
        IXmlCreator xmlCreator,
        IXmlReader xmlReader,
        IDataGenerator dataGenerator) : base(movieRepository)
    {
        _movieRepository = movieRepository;
        _csvCreator = csvCreator;
        _csvReader = csvReader;
        _xmlCreator = xmlCreator;
        _xmlReader = xmlReader;
        _dataGenerator = dataGenerator;
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
                    case "9":
                        AddSampleItemsToRepository();
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
            finally
            {
                _xmlReader.MoviesXmlFileRead -= PrintMessageOnMoviesXmlFileRead!;
                _xmlCreator.MoviesXmlFileCreated -= PrintMessageOnMoviesXmlFileCreated!;
                _csvReader.ReadMoviesCsvFileEvent -= PrintMessageOnReadMoviesCsvFile!;
                _csvCreator.MoviesCsvFileCreated -= PrintMessageOnMoviesCsvFileCreated!;
                _movieRepository.ItemAdded -= MovieAddedOnItemAdded!;
                _movieRepository.ItemRemoved -= MovieRemovedOnItemRemoved!;
            }
        } while (choise != "Q");
    }

    protected override void AddSampleItemsToRepository()
    {
        var counter = 0;
        foreach (var movie in _dataGenerator.GenerateSampleMovies())
        {
            if(IsMovieExistsInRepository(movie.Title!, movie.Year))
            {
                continue;
            }
            counter++;
            _movieRepository.Add(movie);
        }
        _movieRepository.Save();

        MenuHelper.AddSeparator();
        Console.WriteLine($"INFO : {counter} sample {FILENAME} added to repository.");
    }

    protected override void ReadXmlFile()
    {
        MenuHelper.AddSeparator();
        _xmlReader.MoviesXmlFileRead += PrintMessageOnMoviesXmlFileRead!;

        var movies = _xmlReader.ReadMoviesXmlFile();
        foreach (var movie in movies)
        {
            Console.WriteLine(movie);
        }
    }

    protected override void CreateXmlFile()
    {
        MenuHelper.AddSeparator();
        _xmlCreator.MoviesXmlFileCreated += PrintMessageOnMoviesXmlFileCreated!;
        _xmlCreator.CreateMoviesXmlFileFromRepository();

    }

    protected override void ReadCsvFile()
    {
        MenuHelper.AddSeparator();
        _csvReader.ReadMoviesCsvFileEvent += PrintMessageOnReadMoviesCsvFile!;

        var movies = _csvReader.ReadMoviesCsvFile();
        foreach (var movie in movies)
        {
            Console.WriteLine(movie);
        }
    }

    protected override void CreateCsvFile()
    {
        MenuHelper.AddSeparator();
        _csvCreator.MoviesCsvFileCreated += PrintMessageOnMoviesCsvFileCreated!;
        _csvCreator.CreateMoviesCsvFileFromRepository();
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
            _movieRepository.ItemRemoved += MovieRemovedOnItemRemoved!;
            _movieRepository.Remove(movie!);
            _movieRepository.Save();
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

        if (IsMovieExistsInRepository(title, newYear))
        {
            MenuHelper.AddSeparator();
            throw new ArgumentException("ERROR : Movie exist in repository!");
        }

        _movieRepository.ItemAdded += MovieAddedOnItemAdded!;
        _movieRepository.Add(new Movie { Title = title, Year = newYear, Universe = universe, BoxOffice = newBoxOffice });
        _movieRepository.Save();
    }

    private bool IsMovieExistsInRepository(string title, int year)
    {
        if (_movieRepository.GetAll().Where(x => x.Title == title && x.Year == year).Any())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MovieRemovedOnItemRemoved(object sender, Movie movie)
    {
        Console.WriteLine("EVENT INFO : Movie removed successfully.");
    }

    private void MovieAddedOnItemAdded(object sender, Movie movie)
    {
        MenuHelper.AddSeparator();
        Console.WriteLine($"EVENT INFO : New movie added to repository.\n\n{movie}");
    }

    private void PrintMessageOnMoviesXmlFileRead(object sender, EventArgs e)
    {
        Console.WriteLine($"EVENT INFO : {FILENAME}.xml file read successfully.");
        MenuHelper.AddSeparator();
    }

    private void PrintMessageOnMoviesXmlFileCreated(object sender, EventArgs e)
    {
        Console.WriteLine($"EVENT INFO : {FILENAME}.xml file created successfully.");
    }

    private void PrintMessageOnMoviesCsvFileCreated(object sender, EventArgs e)
    {
        Console.WriteLine($"EVENT INFO : {FILENAME}.csv file created successfully.");
    }

    private void PrintMessageOnReadMoviesCsvFile(object sender, EventArgs e)
    {
        Console.WriteLine($"EVENT INFO : {FILENAME}.csv file read successfully.");
        MenuHelper.AddSeparator();
    }
}

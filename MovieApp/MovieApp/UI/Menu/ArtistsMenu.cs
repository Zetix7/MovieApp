using MovieApp.AplicationServices.Components.FileCreator.CsvFile;
using MovieApp.AplicationServices.Components.FileCreator.XmlFile;
using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;
using MovieApp.UI.Menu.Extensions;

namespace MovieApp.UI.Menu;

public class ArtistsMenu : Menu<Artist>
{
    private readonly IRepository<Artist> _artistRepository;
    private readonly ICsvCreator _csvCreator;
    private readonly ICsvReader _csvReader;
    private readonly IXmlCreator _xmlCreator;
    private readonly IXmlReader _xmlReader;

    public ArtistsMenu(IRepository<Artist> artistRepository,
        ICsvCreator csvCreator,
        ICsvReader csvReader,
        IXmlCreator xmlCreator,
        IXmlReader xmlReader) : base(artistRepository)
    {
        _artistRepository = artistRepository;
        _csvCreator = csvCreator;
        _csvReader = csvReader;
        _xmlCreator = xmlCreator;
        _xmlReader = xmlReader;
    }

    public override void LoadMenu()
    {
        Console.WriteLine($"\n------- Artists Menu -------\n");

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
        MenuHelper.AddSeparator();
        var artists = _xmlReader.ReadArtistsXmlFile();
        foreach (var artist in artists)
        {
            Console.WriteLine(artist);
        }
    }

    protected override void CreateXmlFile()
    {
        MenuHelper.AddSeparator();
        _xmlCreator.CreateArtistsXmlFileFromRepository();
        Console.WriteLine("INFO : Created artists.xml file.");
    }

    protected override void ReadCsvFile()
    {
        MenuHelper.AddSeparator();
        _csvReader.ReadArtistsCsvFileEvent += PrintMessageOnReadArtistsCsvFile!;

        var artists = _csvReader.ReadArtistsCsvFile();
        foreach (var artist in artists)
        {
            Console.WriteLine(artist);
        }
    }

    protected override void CreateCsvFile()
    {
        MenuHelper.AddSeparator();
        _csvCreator.CreateArtistsCsvFileFromRepository();
        Console.WriteLine("INFO : Created artists.csv file.");
    }

    protected override void RemoveItemFromRepository()
    {
        MenuHelper.AddSeparator();
        Console.Write("Insert artist Id to remove from repository: ");
        var id = Console.ReadLine();

        if (!int.TryParse(id, out int newInt))
        {
            MenuHelper.AddSeparator();
            throw new FormatException($"ERROR : Invalid id '{id}'! This is not integer!");
        }

        var artist = _artistRepository.GetById(newInt);

        if (artist == null)
        {
            MenuHelper.AddSeparator();
            throw new ArgumentException("ERROR : Id not exists in repository!");
        }

        Console.WriteLine("\tAre you sure to remove artist?:");
        Console.WriteLine(artist);

        Console.Write("\t\tYour choise (Y/N): ");
        var choise = Console.ReadLine()!.Trim().ToUpper();

        MenuHelper.AddSeparator();
        if (choise == "Y")
        {
            _artistRepository.Remove(artist);
            _artistRepository.Save();
            Console.WriteLine("INFO : Artist removed successfully.");
        }
        else
        {
            Console.WriteLine("INFO : Artist remove aborted.");
        }
    }

    protected override void AddNewItemToRepository()
    {
        MenuHelper.AddSeparator();
        Console.Write("Insert FirstName: ");
        var firstName = Console.ReadLine()!;
        Console.Write("Insert LastName: ");
        var lastName = Console.ReadLine()!;

        var artists = _artistRepository.GetAll();
        if (artists.Where(x => x.FirstName == firstName && x.LastName == lastName).Any())
        {
            MenuHelper.AddSeparator();
            throw new ArgumentException($"ERROR : Artist exists in repository!");
        }

        _artistRepository.Add(new Artist { FirstName = firstName, LastName = lastName });
        _artistRepository.Save();

        MenuHelper.AddSeparator();
        Console.WriteLine($"INFO : Artist added to repository.\n\n{_artistRepository.GetAll().LastOrDefault(x => x.FirstName == firstName)}");
    }

    private void PrintMessageOnReadArtistsCsvFile(object sender, EventArgs e)
    {
        Console.WriteLine("INFO : artists.csv file read successfully.");
        MenuHelper.AddSeparator();
    }
}

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
    private const string FILENAME = "artists";

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
            finally
            {
                _xmlReader.ArtistsXmlFileRead -= PrintMessageOnArtistsXmlFileRead!;
                _xmlCreator.ArtistsXmlFileCreated -= PrintMessageOnArtistsXmlFileCreated!;
                _csvReader.ReadArtistsCsvFileEvent -= PrintMessageOnReadArtistsCsvFile!;
                _csvCreator.ArtistsCsvFileCreated -= PrintMessageOnArtistsCsvFileCreated!;
                _artistRepository.ItemAdded -= ArtistAddedOnItemAdded!;
                _artistRepository.ItemRemoved -= ArtistRemovedOnItemRemoved!;
            }
        } while (choise != "Q");
    }

    protected override void AddSampleItemsToRepository()
    {
        throw new NotImplementedException();
    }

    protected override void ReadXmlFile()
    {
        MenuHelper.AddSeparator();
        _xmlReader.ArtistsXmlFileRead += PrintMessageOnArtistsXmlFileRead!;

        var artists = _xmlReader.ReadArtistsXmlFile();
        foreach (var artist in artists)
        {
            Console.WriteLine(artist);
        }
    }

    protected override void CreateXmlFile()
    {
        MenuHelper.AddSeparator();
        _xmlCreator.ArtistsXmlFileCreated += PrintMessageOnArtistsXmlFileCreated!;
        _xmlCreator.CreateArtistsXmlFileFromRepository();
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
        _csvCreator.ArtistsCsvFileCreated += PrintMessageOnArtistsCsvFileCreated!;
        _csvCreator.CreateArtistsCsvFileFromRepository();
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
            _artistRepository.ItemRemoved += ArtistRemovedOnItemRemoved!;
            _artistRepository.Remove(artist);
            _artistRepository.Save();
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

        _artistRepository.ItemAdded += ArtistAddedOnItemAdded!;
        _artistRepository.Add(new Artist { FirstName = firstName, LastName = lastName });
        _artistRepository.Save();
    }

    private void ArtistRemovedOnItemRemoved(object sender, Artist artist)
    {
        Console.WriteLine("EVENT INFO : Artist removed successfully.");
    }

    private void ArtistAddedOnItemAdded(object sender, Artist artist)
    {
        MenuHelper.AddSeparator();
        Console.WriteLine($"IVENT INFO : Artist added to repository.\n\n{artist}");
    }

    private void PrintMessageOnArtistsXmlFileRead(object sender, EventArgs e)
    {
        Console.WriteLine($"EVENT INFO : {FILENAME}.xml file read successfully.");
        MenuHelper.AddSeparator();
    }

    private void PrintMessageOnArtistsXmlFileCreated(object sender, EventArgs e)
    {
        Console.WriteLine($"EVENT INFO : {FILENAME}.xml file created successfully.");
    }

    private void PrintMessageOnArtistsCsvFileCreated(object sender, EventArgs e)
    {
        Console.WriteLine($"EVENT INFO : {FILENAME}.csv file created successfully.");
    }

    private void PrintMessageOnReadArtistsCsvFile(object sender, EventArgs e)
    {
        Console.WriteLine($"EVENT INFO : {FILENAME}.csv file read successfully.");
        MenuHelper.AddSeparator();
    }
}

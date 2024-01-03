using MovieApp.AplicationServices.Components.FileCreator.CsvFile;
using MovieApp.AplicationServices.Components.FileCreator.XmlFile;
using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;
using MovieApp.UI.Menu.Extensions;
using System.Xml;

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
                        RemoveItemToRepository();
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

    protected override void CreateXmlFile()
    {
    }

    protected override void ReadXmlFile()
    {
    }

    protected override void CreateCsvFile()
    {
    }

    protected override void ReadCsvFile()
    {
    }

    protected override void RemoveItemToRepository()
    {
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
        Console.WriteLine($"INFO : Artist added to repository.\n\n{artists.LastOrDefault(x => x.FirstName == firstName && x.LastName == lastName)}");
    }
}

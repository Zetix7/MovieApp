using MovieApp.AplicationServices.Components.FileCreator.CsvFile;
using MovieApp.AplicationServices.Components.FileCreator.XmlFile;
using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;
using MovieApp.UI.Menu.Extensions;

namespace MovieApp.UI.Menu;

public class ArtistsMenu : Menu<Artist>
{
    public ArtistsMenu(IRepository<Artist> artistRepository,
        ICsvCreator csvCreator,
        ICsvReader csvReader,
        IXmlCreator xmlCreator,
        IXmlReader xmlReader) : base(artistRepository)
    {
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
    }
}

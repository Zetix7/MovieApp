using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;

namespace MovieApp.UI.Menu;

public class Menu<T> : IMenu<T> where T : class, IEntity, new()
{
    private readonly IRepository<T> _repository;

    public Menu(IRepository<T> repository)
    {
        _repository = repository;
    }

    public void LoadMenu()
    {
        Console.WriteLine($"\n------- {typeof(T).Name}s Menu -------\n");

        string choise;
        do
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Choose one option:");
            Console.WriteLine($"\t1. Get all {typeof(T).Name}s from repository.");
            Console.WriteLine($"\t2. Get by Id {typeof(T).Name} from repository.");
            Console.WriteLine($"\t3. Add new {typeof(T).Name} to repository.");
            Console.WriteLine($"\t4. Remove exists {typeof(T).Name} in repository.");
            Console.WriteLine($"\t5. Create {typeof(T).Name.ToLower()}s.csv file from repository.");
            Console.WriteLine($"\t6. Read all {typeof(T).Name.ToLower()}s from {typeof(T).Name}s.csv file.");
            Console.WriteLine($"\t7. Create {typeof(T).Name.ToLower()}s.xml file from repository.");
            Console.WriteLine($"\t8. Read all {typeof(T).Name.ToLower()}s from {typeof(T).Name}s.xml file.");
            Console.WriteLine("\tQ. Return.");
            Console.Write("\t\tYour choise: ");
            choise = Console.ReadLine()!.Trim().ToUpper();
            Console.WriteLine("-----------------------------------------------------------------------");

            switch (choise)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
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
}

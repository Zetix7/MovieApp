using MovieApp.DataAccess.Data.Entities;

namespace MovieApp.UI.Menu;

public class Menu<T> : IMenu<T> where T : class, IEntity
{
    public void LoadMenu()
    {
        Console.WriteLine($"\n------- {typeof(T).Name}s Menu -------\n");

        string choise;
        do
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Choose one option:");
            Console.WriteLine($"\t1. {typeof(T).Name}s CRUD actions in repository.");
            Console.WriteLine($"\t2. {typeof(T).Name}s *.csv file actions.");
            Console.WriteLine($"\t3. {typeof(T).Name}s *.xml file actions.");
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
                case "Q":
                    break;
                default:
                    Console.WriteLine("Choose one option or you stuck here forever!");
                    break;
            }

        } while (choise != "Q");
    }
}

namespace MovieApp.UI.Menu;

public class Menu : IMenu
{
    public void RunMenu()
    {
        Console.WriteLine("------- Welcome in Movie App -------\n");

        string choise;
        do
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Choose one option:");
            Console.WriteLine("\t1. Movie resources.");
            Console.WriteLine("\tQ. Quit.");
            Console.Write("\t\tYour choise: ");
            choise = Console.ReadLine()!.Trim().ToUpper();

            switch (choise)
            {
                case "1":
                    break;
                case "Q":
                    break;
                default:
                    Console.WriteLine("-----------------------------------------------------------------------");
                    Console.WriteLine("Choose one option or you stuck here forever!");
                    break;
            }

        } while (choise != "Q" );
    }
}

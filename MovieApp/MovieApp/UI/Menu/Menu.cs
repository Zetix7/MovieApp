using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;
using MovieApp.UI.Menu.Extensions;

namespace MovieApp.UI.Menu;

public abstract class Menu<T> : IMenu<T> where T : class, IEntity, new()
{
    private readonly IRepository<T> _repository;

    public Menu(IRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual void LoadMenu()
    {
        MenuHelper.AddSeparator();
        Console.WriteLine("Choose one option:");
        Console.WriteLine($"\t1. Get all {typeof(T).Name}s from repository.");
        Console.WriteLine($"\t2. Get by Id {typeof(T).Name} from repository.");
        Console.WriteLine($"\t3. Add new {typeof(T).Name} to repository.");
        Console.WriteLine($"\t4. Remove exists {typeof(T).Name} in repository.");
        Console.WriteLine($"\t5. Create {typeof(T).Name.ToLower()}s.csv file from repository.");
        Console.WriteLine($"\t6. Read and print all {typeof(T).Name.ToLower()}s from {typeof(T).Name}s.csv file.");
        Console.WriteLine($"\t7. Create {typeof(T).Name.ToLower()}s.xml file from repository.");
        Console.WriteLine($"\t8. Read and print all {typeof(T).Name.ToLower()}s from {typeof(T).Name}s.xml file.");
        Console.WriteLine($"\t9. Add sample {typeof(T).Name.ToLower()}s to repository.");
        Console.WriteLine("\tQ. Return.");
        Console.Write("\t\tYour choise: ");
    }

    protected void PrintAllItems()
    {
        var items = CheckRepositoryContent();

        foreach (var item in items)
        {
            Console.WriteLine($"{item}");
        }
    }

    protected void PrintItemById()
    {
        var items = CheckRepositoryContent();

        Console.Write($"\tInsert Id to print {typeof(T).Name}: ");
        var itemId = Console.ReadLine()!.Trim();

        if (!int.TryParse(itemId, out var id))
        {
            MenuHelper.AddSeparator();
            throw new FormatException($"ERROR : Invalid Id '{itemId}'! This is not integer!");
        }

        var itemToPrint = _repository.GetById(id);

        if (itemToPrint == null)
        {
            MenuHelper.AddSeparator();
            throw new ArgumentException($"ERROR : {typeof(T).Name} with Id = {itemId} not exist!");
        }

        Console.WriteLine();
        Console.WriteLine(itemToPrint);
    }

    protected abstract void AddNewItemToRepository();
    protected abstract void RemoveItemFromRepository();
    protected abstract void CreateCsvFile();
    protected abstract void ReadCsvFile();
    protected abstract void CreateXmlFile();
    protected abstract void ReadXmlFile();
    protected abstract void AddSampleItemsToRepository();

    private IEnumerable<T> CheckRepositoryContent()
    {
        MenuHelper.AddSeparator();
        var items = _repository.GetAll();
        if (!items.Any())
        {
            throw new Exception("INFO : Repository is empty!");
        }

        return items;
    }
}

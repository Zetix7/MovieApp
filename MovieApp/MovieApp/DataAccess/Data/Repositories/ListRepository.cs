using MovieApp.DataAccess.Data.Entities;

namespace MovieApp.DataAccess.Data.Repositories;

public class ListRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly List<T> _listRepository = new();

    public IEnumerable<T> GetAll()
    {
        return _listRepository.OrderBy(x => x.Id).ToList();
    }
    public T GetById(int id)
    {
        return _listRepository.SingleOrDefault(x => x.Id == id, new T { Id = -1 });
    }

    public void Add(T item)
    {
        _listRepository.Add(item);
    }

    public void Remove(T item)
    {
        _listRepository.Remove(item);
    }

    public void Save()
    {
        // Save not necceserly
    }
}

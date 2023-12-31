using MovieApp.DataAccess.Data.Entities;

namespace MovieApp.DataAccess.Data.Repositories;

public interface IRepository<T> where T : class, IEntity, new()
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Add(T item);
    void Remove(T item);
    void Save();
}

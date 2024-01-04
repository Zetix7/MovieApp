using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Data.Entities;

namespace MovieApp.DataAccess.Data.Repositories;

public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public event EventHandler<T?> ItemAdded;
    public event EventHandler<T?> ItemRemoved;

    public SqlRepository(MovieAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
        _dbSet = _dbContext.Set<T>();
    }

    public IEnumerable<T> GetAll() => _dbSet.OrderBy(x => x.Id).ToList();

    public T? GetById(int id) => _dbSet.SingleOrDefault(x => x.Id == id);

    public void Add(T item)
    {
        _dbSet.Add(item);
        ItemAdded?.Invoke(this, item);
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
        ItemRemoved?.Invoke(this, item);
    }

    public void Save() => _dbContext.SaveChanges();
}

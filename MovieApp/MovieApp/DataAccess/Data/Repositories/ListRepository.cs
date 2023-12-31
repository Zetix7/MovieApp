﻿using MovieApp.DataAccess.Data.Entities;

namespace MovieApp.DataAccess.Data.Repositories;

public class ListRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly List<T> _items = new();

    public IEnumerable<T> GetAll()
    {
        return _items.OrderBy(x => x.Id).ToList();
    }

    public T? GetById(int id)
    {
        return _items.SingleOrDefault(x => x.Id == id, new T { Id = -1 });
    }

    public void Add(T item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
    }

    public void Remove(T item)
    {
        _items.Remove(item);
    }

    public void Save()
    {
        // Save not necceserly
    }
}

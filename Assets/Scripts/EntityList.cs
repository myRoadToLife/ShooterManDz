using System.Collections.Generic;
using System;

public class EntityList <T> 
{
    public event Action OnEntityAdded;
    public event Action OnEntityRemoved;

    private List<T> _entities = new List<T>();

    public int Count => _entities.Count;

    public void Add(T entity)
    {
        if (!_entities.Contains(entity))
        {
            _entities.Add(entity);
            OnEntityAdded?.Invoke();
        }
    }

    public void Remove(T entity)
    {
        if (_entities.Contains(entity))
        {
            _entities.Remove(entity);
            OnEntityRemoved?.Invoke();
        }
    }
}

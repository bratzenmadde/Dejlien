﻿using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DataLogicLayer.Repositories;

namespace DataLogicLayer.Framework
{
    public abstract class Repository<T, TKey> where T : class, IEntity<TKey>
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public DbSet<T> Items => context.Set<T>();

        public T Get(TKey id)
        {
            return Items.Find(id);
        }

        public List<T> GetAll()
        {
            return Items.ToList();
        }

        public void Add(T item)
        {
            Items.Add(item);
        }

        public void Remove(TKey id)
        {
            var item = Get(id);
            Items.Remove(item);
        }

        public void Edit(T item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}

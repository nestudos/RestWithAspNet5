using Microsoft.EntityFrameworkCore;
using RestWithAspNet5.Model.Base;
using RestWithAspNet5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        private MySqlContext _context;

        private DbSet<T> _dataset;
        public GenericRepository(MySqlContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                _dataset.Add(item);
                _context.SaveChanges();
                return item;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(long id)
        {
            var result = _dataset.SingleOrDefault(p => p.Id == id);

            if (result == null)
            {
                return;
            }

            try
            {
                _dataset.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public List<T> FindAll()
        {
            return _dataset.ToList();
        }

        public T FindById(long id)
        {
            return _dataset.SingleOrDefault(p => p.Id == id);
        }

        public T Update(T item)
        {
            var result = _dataset.SingleOrDefault(p => p.Id == item.Id);

            if (result == null)
            {
                return null;
            }

            try
            {
                _context.Entry(result).CurrentValues.SetValues(item);
                _context.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(long id)
        {
            return _dataset.Any(p => p.Id == id);
        }
    }
}

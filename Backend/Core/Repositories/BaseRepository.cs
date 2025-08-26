using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Interfaces;
using EntityLayer.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace EntityLayer.Repositories
{
    public class BaseRepository<T> : EntityBaseRepository<T> where T : IentityBase
    {
        protected ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> getQueryable()
        {
            return _context.Set<T>();
        }
        public async Task<IEnumerable<T>> getAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> getByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public void add(T entity)
        {
            _context.Add(entity);
        }

        public void remove(T entity)
        {
            _context.Remove(entity);
        }

        public void update(T entity)
        {
            _context.Update(entity);
        }
    }
}

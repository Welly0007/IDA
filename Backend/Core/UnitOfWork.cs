using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Interfaces;
using EntityLayer.AppDbContext;
using EntityLayer.Repositories;

namespace EntityLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }
        public EntityBaseRepository<TEntity> CreateRepository<TEntity>() where TEntity : IentityBase
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new BaseRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }
            return (_repositories[type] as EntityBaseRepository<TEntity>)!;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

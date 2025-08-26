using Core;
using Core.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.Contexts;
using System.Linq.Expressions;

namespace Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()

    {
        private readonly ASCenterContext dataContext;

        public EntityBaseRepository(ASCenterContext dataContext)
        {
            this.dataContext = dataContext;
        }


        public virtual T Find(params object[] KeyValue)
        {
            return dataContext.Set<T>().Find(KeyValue);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dataContext.Set<T>();
        }

        public virtual IQueryable<T> All
        {
            get
            {
                return GetAll();
            }
        }
        public ASCenterContext DbContext2
        {
            get { return dataContext ; }
        }
        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dataContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dataContext.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            dataContext.Set<T>().Add(entity);

        }
        public virtual void Add(List<T> entities)
        {
            foreach (var e in entities)
            {
                dataContext.Set<T>().Add(e);
            }
        }

        public virtual void AddBulk(List<T> entities)
        {

            dataContext.Set<T>().AddRange(entities);
        }

        public virtual void Edit(T entity)
        {
            dataContext.Set<T>().Update(entity);
        }

        public virtual void Delete(T entity)
        {
            dataContext.Set<T>().Remove(entity);
        }

        public virtual void Delete(List<T> entities)
        {
            foreach (var e in entities)
            {
                dataContext.Set<T>().Remove(e);
            }
        }

        public virtual void DeleteRange(List<T> entities)
        {

            dataContext.Set<T>().RemoveRange(entities);

        }


        public T FirstOrDefault(Expression<Func<T, bool>> predicat)
        {
            return dataContext.Set<T>().FirstOrDefault(predicat);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicat)
        {
            return dataContext.Set<T>().Where(predicat);
        }

        public TResult Max<TResult>(Expression<Func<T, TResult>> selector)
        {
            return dataContext.Set<T>().Max(selector);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await dataContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
    }
}

namespace CoreLayer.Interfaces
{
    public interface EntityBaseRepository<T> where T : IentityBase
    {
        IQueryable<T> getQueryable();
        Task<IEnumerable<T>> getAllAsync();
        Task<T?> getByIdAsync(int id);
        void add(T entity);
        void remove(T entity);
        void update(T entity);

    }
}

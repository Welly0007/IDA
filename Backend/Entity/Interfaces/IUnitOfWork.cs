namespace CoreLayer.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        EntityBaseRepository<TEntity> CreateRepository<TEntity>() where TEntity : IentityBase;
        Task<int> CompleteAsync();

    }
}

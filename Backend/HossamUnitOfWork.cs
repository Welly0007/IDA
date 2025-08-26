using Core;
using Core.Interfaces;
using Core.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repositories.Contexts;
using Repositories.Infrastructure;
using System.Collections;
using System.Data;

namespace Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ASCenterContext dbContext;
        private readonly IHttpContextAccessor httpContext;
        private Hashtable _repository;
        private UnitOfWork currentUnitOfWork;



        public UnitOfWork(ASCenterContext dbContext, IHttpContextAccessor httpContext)
        {
            this.dbContext = dbContext;
            this.httpContext = httpContext;
            _repository = new Hashtable();
            currentUnitOfWork = this;
        }
        public IEntityBaseRepository<TEntity> CreateRepository<TEntity>() where TEntity : class, IEntityBase, new()
        {
            var Type = typeof(TEntity).Name;
            if (!_repository.ContainsKey(Type))
            {
                var NewRepository = new EntityBaseRepository<TEntity>(dbContext);
                _repository.Add(Type, NewRepository);
            }
            return _repository[Type] as IEntityBaseRepository<TEntity>;
        }

        public  void AuditingCommit()
        {

            Auditing audit = new Auditing(httpContext);
            audit.WriteAuditing(dbContext);
            int op = dbContext.SaveChanges();


        }
        public  int AuditingCommit2()
        {
            int op = 0;
            Auditing audit = new Auditing(httpContext);
            audit.WriteAuditing(dbContext);
            return op = dbContext.SaveChanges();
        }

        public void Commit()
        {

            try
            {
                var result = dbContext.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("An error occurred while committing changes to the database.", ex);
            }
        }

        public IQueryable<T> ExecQueryStr<T>(string query)
        {
            return dbContext.Database.SqlQueryRaw<T>(query);
        }

        public IQueryable<T> ExecQueryPar<T>(string query, object[] parameters)
        {
            return dbContext.Database.SqlQueryRaw<T>(query, parameters);
        }
        public IQueryable<T> ExecQueryPar<T>(FormattableString query)
        {
            return dbContext.Database.SqlQuery<T>(query);
        }

        public void ExecSqlCommand(string query, object[] parameters = null)
        {
            if (parameters == null)
                dbContext.Database.ExecuteSqlRaw(query);
            else
                dbContext.Database.ExecuteSqlRaw(query, parameters);
        }

        public Task<int> ExecSqlCommandAsync(string query)
        {
            return dbContext.Database.ExecuteSqlRawAsync(query);
        }

        public void ExecSqlCommandAsync(string query, object[] parameters = null)
        {
            if (parameters == null)
                dbContext.Database.ExecuteSqlRawAsync(query);
            else
                dbContext.Database.ExecuteSqlRaw(query, parameters);
        }

        public DataTable ExecuteQueryToDataTable(string query, Dictionary<string, object> Parameters = null)
        {
            SqlCommand command = new SqlCommand(query, new SqlConnection(""));
            command.CommandType = CommandType.Text;
            if (Parameters != null)
            {
                foreach (var Key in Parameters.Keys)
                {
                    command.Parameters.Add(new SqlParameter(Key, Parameters[Key]));
                }
            }
            SqlDataAdapter Adapter = new SqlDataAdapter(command);
            DataTable DT = new DataTable();
            Adapter.Fill(DT);
            return DT;
        }



        public IDbContextTransaction BeginTransactionScope()
        {
            try
            {
                return dbContext.Database.BeginTransaction();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

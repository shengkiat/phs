using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PHS.Repository.Interface.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties);

        Task<TEntity> GetAsync(int id);

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        //Task AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
        //Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        //Task RemoveAsync(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
        //Task RemoveRangeAsync(IEnumerable<TEntity> entities);
    }
}

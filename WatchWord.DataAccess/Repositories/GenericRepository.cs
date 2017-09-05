using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WatchWord.DataAccess.Abstract;
using WatchWord.Domain.Entity.Common;

namespace WatchWord.DataAccess.Repositories
{
    public abstract class GenericRepository<TEntity, TIdentity> : IGenericRepository<TEntity, TIdentity> where TEntity : Entity<TIdentity>, new()
    {
        private readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>Initializes a new instance of the <see cref="GenericRepository{TEntity,TIdentity}"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        protected GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        #region CREATE

        public virtual async void Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async void Insert(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        #endregion

        #region READ

        public virtual bool Any(Expression<Func<TEntity, bool>> whereProperties = null)
        {
            return _dbSet.Any(whereProperties);
        }

        public virtual async Task<int> GetCount(Expression<Func<TEntity, bool>> whereProperties = null)
        {
            return await AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties).CountAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties, includeProperties).ToListAsync();
        }

        public virtual async Task<List<TEntity>> SkipAndTakeAsync(int skipNumber, int amount, Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties, includeProperties).Skip(skipNumber).Take(amount).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(TIdentity id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetByConditionAsync(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entity = await AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties, includeProperties).FirstOrDefaultAsync();
            if (entity != null && _context.Entry(entity).State == EntityState.Detached)
            {
                _context.Entry(entity).State = EntityState.Unchanged;
            }
            return entity;
        }

        public async Task<ICollection<TEntity>> GetRandomEntititiesByConditionAsync(int count = 1, Expression<Func<TEntity, bool>> whereProperties = null)
        {
            return await AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties)
              .OrderBy(e => Guid.NewGuid()).Select(e => e).Take(count).ToListAsync();
        }

        #endregion

        #region UPDATE

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Update(entityToUpdate);
        }

        #endregion

        #region DELETE

        public virtual void Delete(TIdentity id)
        {
            var entity = new TEntity { Id = id };
            Delete(entity);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(IEnumerable<TEntity> entitiesToDelete)
        {
            _dbSet.RemoveRange(entitiesToDelete);
        }

        #endregion

        /// <summary>Makes query to table by using "where" predicate and "include(join) properties".</summary>
        /// <typeparam name="TSource">Entity type.</typeparam>
        /// <param name="table">Entity framework table.</param>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns></returns>
        private static IQueryable<TSource> AggregateQueryProperties<TSource>(IQueryable<TSource> table, Expression<Func<TSource, bool>> whereProperties = null,
            params Expression<Func<TSource, object>>[] includeProperties) where TSource : Entity<TIdentity>
        {
            //TODO: IOrderedQueryable param
            var query = includeProperties.Aggregate(table, (current, includeProperty) => current.Include(includeProperty));

            if (whereProperties != null)
            {
                query = query.Where(whereProperties);
            }

            return query.OrderBy(e => e.Id);
        }
    }
}
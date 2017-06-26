using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WatchWord.Domain.Entity.Common;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represent entity framework generic repository pattern.</summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TIdentity">Type of entity Id.</typeparam>
    public abstract class GenericRepository<TEntity, TIdentity> where TEntity : Entity<TIdentity>, new()
    {
        private DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>Initializes a new instance of the <see cref="EfGenericRepository{TEntity,TId}"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        protected GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        #region CREATE

        /// <summary>Inserts entity asynchronously.</summary>
        /// <param name="entity">The entity.</param>
        public async virtual void Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>Inserts entities asynchronously.</summary>
        /// <param name="entities">The entities enumerable.</param>
        public async void Insert(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        #endregion

        #region READ

        /// <summary>Gets count of entities from database asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        public virtual async Task<int> GetCount(Expression<Func<TEntity, bool>> whereProperties = null)
        {
            return await AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties).CountAsync();
        }

        /// <summary>Gets entities from database asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties, includeProperties).ToListAsync();
        }

        /// <summary>Skip the given number and returns the specified number of entities from database asynchronously.</summary>
        /// <param name="skipNumber">Number of entities to skip.</param>
        /// <param name="amount">Number of entities to take.</param>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        public virtual async Task<List<TEntity>> SkipAndTakeAsync(int skipNumber, int amount, Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties, includeProperties).Skip(skipNumber).Take(amount).ToListAsync();
        }

        /// <summary>Finds entity by id asynchronously.</summary>
        /// <param name="id">The id.</param>
        /// <returns>The operation <see cref="TEntity"/>.</returns>
        public virtual async Task<TEntity> GetById(TIdentity id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>Gets the first entity which matchs the condition asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        public virtual async Task<TEntity> GetByСondition(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties, includeProperties).FirstOrDefaultAsync();
        }

        #endregion

        #region UPDATE

        /// <summary>Updates entity in database.</summary>
        /// <param name="entityToUpdate">Entity to update.</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Update(entityToUpdate);
        }

        #endregion

        #region DELETE

        /// <summary>Deletes entity from database by id.</summary>
        /// <param name="id">Entity id.</param>
        public virtual void Delete(TIdentity id)
        {
            var entity = new TEntity { Id = id };
            Delete(entity);
        }

        /// <summary>Deletes entity from database.</summary>
        /// <param name="entityToDelete">Entity to delete.</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        /// <summary>Deletes entities from database.</summary>
        /// <param name="entitiesToDelete">Entities to delete.</param>
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
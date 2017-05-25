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

        /// <summary>Inserts or updates entity.</summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>Inserts entities.</summary>
        /// <param name="entities">The entities enumerable.</param>
        public void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        #endregion

        #region READ

        /// <summary>Gets count of entities from database.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        public virtual int GetCount(Expression<Func<TEntity, bool>> whereProperties = null)
        {
            return AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties).Count();
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

        /// <summary>Gets entities from database.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties, includeProperties).ToList();
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

        /// <summary>Skip the given number and returns the specified number of entities from database.</summary>
        /// <param name="skipNumber">Number of entities to skip.</param>
        /// <param name="amount">Number of entities to take.</param>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        public virtual List<TEntity> SkipAndTake(int skipNumber, int amount, Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties, includeProperties).Skip(skipNumber).Take(amount).ToList();
        }

        /// <summary>Finds entity by id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>The operation <see cref="TEntity"/>.</returns>
        public virtual TEntity GetById(TIdentity id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>Gets the first entity which matchs the condition.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        public virtual TEntity GetByСondition(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return AggregateQueryProperties(_dbSet.AsNoTracking(), whereProperties, includeProperties).FirstOrDefault();
        }

        #endregion

        #region UPDATE

        /// <summary>Updates entity in database.</summary>
        /// <param name="entityToUpdate">Entity to update.</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;
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

        /// <summary>Saves all pending changes.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        public virtual int Save()
        {
            return _context.SaveChanges();
        }

        /// <summary>Saves all pending changes asynchronously.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

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

        /// <summary>Disposes the current object.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Disposes all external resources.</summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;
            _context.Dispose();
            _context = null;
        }
    }
}
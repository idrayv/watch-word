using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WatchWord.Domain.Entity.Common;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represent entity framework generic repository pattern.</summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TIdentity">Type of entity Id.</typeparam>
    public interface IGenericRepository<TEntity, TIdentity> where TEntity : Entity<TIdentity>, new()
    {
        #region CREATE

        /// <summary>Inserts entity asynchronously.</summary>
        /// <param name="entity">The entity.</param>
        void Insert(TEntity entity);

        /// <summary>Inserts entities asynchronously.</summary>
        /// <param name="entities">The entities enumerable.</param>
        void Insert(IEnumerable<TEntity> entities);

        #endregion

        #region READ

        /// <summary>Gets count of entities from database asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        Task<int> GetCount(Expression<Func<TEntity, bool>> whereProperties = null);

        /// <summary>Gets entities from database asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>Skip the given number and returns the specified number of entities from database asynchronously.</summary>
        /// <param name="skipNumber">Number of entities to skip.</param>
        /// <param name="amount">Number of entities to take.</param>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        Task<List<TEntity>> SkipAndTakeAsync(int skipNumber, int amount, Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>Finds entity by id asynchronously.</summary>
        /// <param name="id">The id.</param>
        /// <returns>The operation <see cref="TEntity"/>.</returns>
        Task<TEntity> GetById(TIdentity id);

        /// <summary>Gets the first entity which matchs the condition asynchronously.</summary>
        /// <param name="whereProperties">Where predicate.</param>
        /// <param name="includeProperties">Include properties.</param>
        /// <returns>The list of entities.</returns>
        Task<TEntity> GetByConditionAsync(Expression<Func<TEntity, bool>> whereProperties = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        #endregion

        #region UPDATE

        /// <summary>Updates entity in database.</summary>
        /// <param name="entityToUpdate">Entity to update.</param>
        void Update(TEntity entityToUpdate);

        #endregion

        #region DELETE

        /// <summary>Deletes entity from database by id.</summary>
        /// <param name="id">Entity id.</param>
        void Delete(TIdentity id);

        /// <summary>Deletes entity from database.</summary>
        /// <param name="entityToDelete">Entity to delete.</param>
        void Delete(TEntity entityToDelete);

        /// <summary>Deletes entities from database.</summary>
        /// <param name="entitiesToDelete">Entities to delete.</param>
        void Delete(IEnumerable<TEntity> entitiesToDelete);

        #endregion
    }
}
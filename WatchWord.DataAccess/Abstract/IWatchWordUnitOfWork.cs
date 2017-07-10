using System;
using System.Threading.Tasks;

namespace WatchWord.DataAccess
{
    public interface IWatchWordUnitOfWork
    {
        /// <summary>Returns repository by specified type.</summary>
        /// <typeparam name="T">Repository type.</typeparam>
        /// <returns>Repository instance.</returns>
        T Repository<T>();

        /// <summary>Saves all pending changes asynchronously.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        Task<int> SaveAsync();
    }
}
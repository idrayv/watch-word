using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WatchWord.DataAccess
{
    public class WatchWordUnitOfWork
    {
        private IServiceProvider serviceProvider;
        private DbContext context;

        public WatchWordUnitOfWork(IServiceProvider serviceProvider, DbContext context)
        {
            this.serviceProvider = serviceProvider;
            this.context = context;
        }

        public T Repository<T>() where T : class
        {
            return serviceProvider.GetService<T>();
        }

        /// <summary>Saves all pending changes asynchronously.</summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state.</returns>
        public virtual async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
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
            if (context == null) return;
            context.Dispose();
            context = null;
        }
    }
}
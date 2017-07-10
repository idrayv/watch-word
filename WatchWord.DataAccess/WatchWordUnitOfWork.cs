using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WatchWord.DataAccess
{
    /// <summary>Represents unit of work pattern for WatchWord repositories.</summary>
    public class WatchWordUnitOfWork : IWatchWordUnitOfWork
    {
        private IServiceProvider serviceProvider;
        private DbContext context;

        /// <summary>Initializes a new instance of the <see cref="WatchWordUnitOfWork"/> class.</summary>
        /// <param name="serviceProvider">IoC provider</param>
        /// <param name="context">Entity framework context.</param>
        public WatchWordUnitOfWork(IServiceProvider serviceProvider, DbContext context)
        {
            this.serviceProvider = serviceProvider;
            this.context = context;
        }

        public T Repository<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public virtual async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
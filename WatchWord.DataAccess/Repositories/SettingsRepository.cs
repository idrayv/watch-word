using Microsoft.EntityFrameworkCore;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for settings.</summary>
    public class SettingsRepository : GenericRepository<Setting, int>, ISettingsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="SettingsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public SettingsRepository(DbContext context) : base(context)
        {
        }
    }
}
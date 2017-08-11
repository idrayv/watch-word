using Microsoft.EntityFrameworkCore;
using WatchWord.DataAccess.Abstract;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for translations.</summary>
    public class TranslationsRepository : GenericRepository<Translation, int>, ITranslationsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="Translations"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public TranslationsRepository(DbContext context) : base(context)
        {
        }
    }
}
using Microsoft.EntityFrameworkCore;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for known words.</summary>
    public class KnownWordsRepository : GenericRepository<KnownWord, int>, IKnownWordsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="KnownWordsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public KnownWordsRepository(DbContext context) : base(context)
        {
        }
    }
}
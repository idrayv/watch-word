using Microsoft.EntityFrameworkCore;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for learning words.</summary>
    public class LearnWordsRepository : GenericRepository<LearnWord, int>, ILearnWordsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="LearnWordsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public LearnWordsRepository(DbContext context) : base(context)
        {
        }
    }
}
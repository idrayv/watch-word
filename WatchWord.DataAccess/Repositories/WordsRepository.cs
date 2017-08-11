using Microsoft.EntityFrameworkCore;
using WatchWord.DataAccess.Abstract;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for words.</summary>
    public class WordsRepository : GenericRepository<Word, int>, IWordsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="WordsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public WordsRepository(DbContext context) : base(context)
        {
        }
    }
}
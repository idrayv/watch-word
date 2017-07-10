using Microsoft.EntityFrameworkCore;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for vocabulary words.</summary>
    public class VocabWordsRepository : GenericRepository<VocabWord, int>, IVocabWordsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="VocabWordsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public VocabWordsRepository(DbContext context) : base(context)
        {
        }
    }
}
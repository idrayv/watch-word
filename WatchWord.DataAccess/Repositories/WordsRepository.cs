using Microsoft.EntityFrameworkCore;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    public class WordsRepository : GenericRepository<Word, int>
    {
        public WordsRepository(DbContext context) : base(context)
        {
        }
    }
}
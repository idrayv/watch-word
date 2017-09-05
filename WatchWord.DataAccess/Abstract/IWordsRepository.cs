using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Abstract
{
    public interface IWordsRepository : IGenericRepository<Word, int>
    {
        Task<List<Word>> GetTopWordsByMaterialAsync(int count, int materialId);
    }
}
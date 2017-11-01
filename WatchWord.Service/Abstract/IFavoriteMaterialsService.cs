using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Service.Abstract
{
    public interface IFavoriteMaterialsService
    {
        Task<int> AddAsync(Account account, int materialId);
        Task<bool> IsFavoriteAsync(Account account, int materialId);
        Task<int> DeleteAsync(Account account, int materialId);
    }
}
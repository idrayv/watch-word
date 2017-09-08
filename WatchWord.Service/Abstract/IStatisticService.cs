using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Service.Abstract
{
    public interface IStatisticService
    {
        Task<List<Material>> GetRandomMaterialsAsync(int count);

        Task<List<VocabWord>> GetTop(int count, int materialId, int userId);
    }
}

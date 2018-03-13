using System.Threading.Tasks;

namespace WatchWord.FavoriteMaterials
{
    public interface IFavoriteMaterialAppService
    {
        Task Post(int materialId);

        Task<bool> Get(int materialId);

        Task Delete(int materialId);
    }
}

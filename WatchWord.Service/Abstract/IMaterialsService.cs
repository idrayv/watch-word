using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Service.Abstract
{
    public interface IMaterialsService
    {
        /// <summary>Saves material to the data storage.</summary>
        /// <param name="material">The material <see cref="Material"/>.</param>
        /// <returns>Id of inserted material.</returns>
        Task<int> SaveMaterial(Material material);

        /// <summary>Deletes material from the data storage.</summary>
        /// <param name="id">Material identity.</param>
        /// <returns>Count of deleted materials.</returns>
        Task<int> DeleteMaterial(int id);

        /// <summary>Gets material by Id.</summary>
        /// <param name="id">Material identity.</param>
        /// <returns>Material entity.</returns>
        Task<Material> GetMaterial(int id);

        /// <summary>Gets total count of the materials.</summary>
        /// <returns>Total count of the materials.</returns>
        Task<int> TotalCount();

        /// <summary>Get materials from data storage</summary>
        /// <param name="from">Position of the first material</param>
        /// <param name="count">Count</param>
        /// <returns>Collection of material</returns>
        Task<IEnumerable<Material>> GetMaterials(int from, int count);
    }
}


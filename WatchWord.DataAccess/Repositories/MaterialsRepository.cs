using Microsoft.EntityFrameworkCore;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    /// <summary>Represents repository pattern for materials.</summary>
    public class MaterialsRepository : GenericRepository<Material, int>, IMaterialsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="MaterialsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public MaterialsRepository(DbContext context) : base(context)
        {
        }
    }
}
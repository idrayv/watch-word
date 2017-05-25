using Microsoft.EntityFrameworkCore;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    public class MaterialsRepository : GenericRepository<Material, int>
    {
        public MaterialsRepository(DbContext context) : base(context)
        {
        }
    }
}
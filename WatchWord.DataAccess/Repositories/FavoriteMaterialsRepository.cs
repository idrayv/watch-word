using Microsoft.EntityFrameworkCore;
using WatchWord.DataAccess.Abstract;
using WatchWord.Domain.Entity;

namespace WatchWord.DataAccess.Repositories
{
    public class FavoriteMaterialsRepository : GenericRepository<FavoriteMaterial, int>, IFavoriteMaterialsRepository
    {
        /// <summary>Initializes a new instance of the <see cref="FavoriteMaterialsRepository"/> class.</summary>
        /// <param name="context">Entity framework context.</param>
        public FavoriteMaterialsRepository(DbContext context) : base(context)
        {
        }
    }
}

using Abp.Domain.Entities;
using WatchWord.Authorization.Users;

namespace WatchWord.Entities
{
    public class FavoriteMaterial : Entity<long>
    {
        /// <summary>Gets or sets the material.</summary>
        public long MaterialId { get; set; }
        public virtual Material Material { get; set; }

        /// <summary>Gets or sets the user account.</summary>
        public long AccountId { get; set; }
        public virtual User Account { get; set; }
    }
}

using Abp.Domain.Entities;
using WatchWord.Authorization.Users;

namespace WatchWord.Domain.Entities
{
    public class FavoriteMaterial : Entity<long>
    {
        /// <summary>Gets or sets the material.</summary>
        public virtual Material Material { get; set; }

        /// <summary>Gets or sets the user account.</summary>
        public virtual User Account { get; set; }
    }
}

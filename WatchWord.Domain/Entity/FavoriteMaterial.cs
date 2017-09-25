using WatchWord.Domain.Entity.Common;

namespace WatchWord.Domain.Entity
{
    public class FavoriteMaterial : Entity<int>
    {
        /// <summary>Gets or sets the material.</summary>
        public virtual Material Material { get; set; }

        /// <summary>Gets or sets the user account.</summary>
        public virtual Account Account { get; set; }
    }
}
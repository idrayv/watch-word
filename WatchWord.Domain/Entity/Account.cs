using WatchWord.Domain.Entity.Common;

namespace WatchWord.Domain.Entity
{
    /// <summary>WatchWord user's account entity.</summary>
    public class Account : Entity<int>
    {
        /// <summary>Gets or sets the user's id.</summary>
        public int ExternalId { get; set; }
    }
}
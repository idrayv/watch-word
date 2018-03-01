using Abp.Domain.Entities;
using System.Collections.Generic;

namespace WatchWord.Domain.Entities
{
    /// <summary>WatchWord user's account entity.</summary>
    public class Account : Entity<long>
    {
        /// <summary>Gets or sets the user's id.</summary>
        public long ExternalId { get; set; }

        /// <summary>Gets or sets the user's Name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the collection of materials.</summary>
        public virtual ICollection<Material> Materials { get; set; }

        /// <summary>Gets or sets the collection of settings.</summary>
        public virtual ICollection<WatchWordSetting> Settings { get; set; }

        /// <summary>Gets or sets the collection of vocab words.</summary>
        public virtual ICollection<VocabWord> VocabWords { get; set; }

        /// <summary>Gets or sets the collection of favorite materials.</summary>
        public virtual ICollection<FavoriteMaterial> FavoriteMaterials { get; set; }
    }
}

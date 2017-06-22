using System.Collections.Generic;
using WatchWord.Domain.Entity.Common;

namespace WatchWord.Domain.Entity
{
    /// <summary>Material type, film or series.</summary>
    public enum MaterialType
    {
        /// <summary>The film.</summary>
        Film,
        /// <summary>The series.</summary>
        Series
    }

    /// <summary>The Material entity, a film or series with information about it.</summary>
    public class Material : Entity<int>
    {
        /// <summary>Gets or sets the material type.</summary>
        public MaterialType Type { get; set; }

        /// <summary>Gets or sets name of the material.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the description of the material.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the image of the material.</summary>
        public string Image { get; set; }

        /// <summary>Gets or sets the creator of the material.</summary>
        public virtual Account Owner { get; set; }

        /// <summary>Gets or sets the collection of words.</summary>
        public virtual ICollection<Word> Words { get; set; }
    }
}
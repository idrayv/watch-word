using System.Collections.Generic;
using Abp.Domain.Entities;
using WatchWord.Authorization.Users;

namespace WatchWord.Domain.Entities
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
    public class Material : Entity<long>
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
        public virtual User Owner { get; set; }

        /// <summary>Gets or sets the collection of words.</summary>
        public virtual List<Word> Words { get; set; }

        /// <summary>Gets or sets the collection of subtitle files.</summary>
        public virtual List<SubtitleFile> SubtitleFiles { get; set; }

        /// <summary>Gets or sets the collection of favorite materials.</summary>
        public virtual List<FavoriteMaterial> FavoriteMaterials { get; set; }
    }
}

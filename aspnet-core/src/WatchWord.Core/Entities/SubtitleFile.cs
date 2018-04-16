using Abp.Domain.Entities;

namespace WatchWord.Entities
{
    /// <summary>Stores full text of subtitle file.</summary>
    public class SubtitleFile : Entity<long>
    {
        /// <summary>Gets or sets the material.</summary>
        public virtual Material Material { get; set; }

        /// <summary>Gets or sets subtitle text.</summary>
        public string SubtitleText { get; set; }
    }
}

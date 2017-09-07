using WatchWord.Domain.Entity.Common;

namespace WatchWord.Domain.Entity
{
    /// <summary>Stores full text of subtitle file.</summary>
    public class SubtitleFile : Entity<int>
    {
        /// <summary>Gets or sets the material.</summary>
        public virtual Material Material { get; set; }

        /// <summary>Gets or sets subtitle text.</summary>
        public string SubtitleText { get; set; }
    }
}
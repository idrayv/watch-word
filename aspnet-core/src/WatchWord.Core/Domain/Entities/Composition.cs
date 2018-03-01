using Abp.Domain.Entities;

namespace WatchWord.Domain.Entities
{
    /// <summary>A pointer to a specific word in the file.</summary>
    public class Composition : Entity<long>
    {
        /// <summary>Gets or sets link to the words table.</summary>
        public virtual Word Word { get; set; }

        /// <summary>Gets or sets the serial number of the line that contains the word.</summary>
        public int Line { get; set; }

        /// <summary>Gets or sets the position of the first character in word from the beginning of the line.</summary>
        public int Column { get; set; }
    }
}

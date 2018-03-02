using System.Collections.Generic;
using Abp.Domain.Entities;

namespace WatchWord.Domain.Entities
{
    /// <summary>Word in the material Entity.</summary>
    public class Word : Entity<long>
    {
        /// <summary>Gets or sets the material.</summary>
        public virtual Material Material { get; set; }

        /// <summary>Gets or sets the Good, the bad and the word.</summary>
        public string TheWord { get; set; }

        /// <summary>Gets or sets count of words in material.</summary>
        public int Count { get; set; }

        /// <summary>Gets or sets the collection of compositions.</summary>
        public virtual List<Composition> Compositions { get; set; }
    }
}

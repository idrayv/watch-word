using WatchWord.Domain.Entities.Common;

namespace WatchWord.Domain.Entities
{
    /// <summary>Word statistic.</summary>
    public class WordStatistic : Entity<int>
    {
        /// <summary>Gets or sets word.</summary>
        public string Word { get; set; }

        /// <summary>Gets or sets total count of word within all materials.</summary>
        public int TotalCount { get; set; }

        /// <summary>Gets or sets total count of word within user known words vocabs.</summary>
        public int KnownCount { get; set; }

        /// <summary>Gets or sets total count of word within user learn words vocabs.</summary>
        public int LearnCount { get; set; }
    }
}

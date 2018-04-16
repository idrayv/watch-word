using Abp.Domain.Entities;
using WatchWord.Authorization.Users;

namespace WatchWord.Entities
{
    /// <summary>The word from vocabulary of words.</summary>
    public class VocabWord : Entity<long>
    {
        /// <summary>Gets or sets the original word.</summary>
        public string Word { get; set; }

        /// <summary>Gets or sets translation of the word.</summary>
        public string Translation { get; set; }

        /// <summary>Gets or sets the owner of the vocabulary word.</summary>
        public virtual User Owner { get; set; }

        /// <summary>Gets or sets the type of the vocabulary word. NewWord, LearnWord or KnownWord.</summary>
        public VocabType Type { get; set; }
    }

    public enum VocabType
    {
        LearnWord,
        KnownWord,
        UnsignedWord,
        IgnoredWord
    }
}

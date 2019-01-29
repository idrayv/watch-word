using Abp.Domain.Entities;
using WatchWord.Authorization.Users;

namespace WatchWord.Entities
{
    public class VocabWordStatistic : Entity<long>
    {
        public string Word { get; set; }

        public long OwnerId { get; set; }

        public User Owner { get; set; }

        public int CorrectGuesses { get; set; }

        public int WrongGuesses { get; set; }
    }
}

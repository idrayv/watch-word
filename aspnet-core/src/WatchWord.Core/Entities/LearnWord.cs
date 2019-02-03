namespace WatchWord.Entities
{
    public class LearnWord : VocabWord
    {
        public int CorrectGuessesCount { get; set; }

        public int WrongGuessesCount { get; set; }
    }
}

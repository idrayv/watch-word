namespace WatchWord.Models
{
    public class VocabularyRequestModel
    {
        public string Word { get; set; }
        public string Translation { get; set; }
        public bool IsKnown { get; set; }
    }
}
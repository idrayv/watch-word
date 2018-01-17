using System.ComponentModel.DataAnnotations;

namespace WatchWord.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
using System.Collections.Generic;
using WatchWord.Infrastructure;

namespace WatchWord.Models
{
    public class BaseResponseModel
    {
        public bool Success { get; set; } 
        public List<string> Errors { get; set; } = new List<string>();

        public string ToJson() => ApiJsonSerializer.Serialize(this);
    }
}
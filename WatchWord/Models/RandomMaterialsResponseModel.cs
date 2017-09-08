using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Models
{
    public class RandomMaterialsResponseModel: BaseResponseModel
    {
        public List<Material> Materials { get; set; }
    }
}

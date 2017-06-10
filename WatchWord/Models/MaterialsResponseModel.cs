using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Models
{
    public class MaterialsResponseModel: BaseResponseModel
    {
        public IEnumerable<Material> Materials { get; set; }
    }
}
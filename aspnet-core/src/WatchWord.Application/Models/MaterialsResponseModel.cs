using System.Collections.Generic;
using WatchWord.Domain.Entities;

namespace WatchWord.Models
{
    public class MaterialsResponseModel
    {
        public IEnumerable<Material> Materials { get; set; }
    }
}

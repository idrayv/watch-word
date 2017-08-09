using System.Collections.Generic;

namespace WatchWord.Models
{
    public class EntitiesResponseModel<TEntity>: BaseResponseModel
    {
        public List<TEntity> Entities { get; set; } = new List<TEntity>();
    }
}
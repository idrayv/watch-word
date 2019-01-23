using System.Collections.Generic;
using Abp.Domain.Entities;
using WatchWord.Authorization.Users;
using WatchWord.Entities;
using WatchWord.Users.Dto;

namespace WatchWord.Materials.Dto
{
    public class MaterialDto : Entity<long>
    {
        public MaterialType Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public virtual UserDto Owner { get; set; }

        /// <summary>Gets or sets the collection of words.</summary>
        public virtual List<Word> Words { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WatchWord.Domain.Entity;
using WatchWord.ModelBinders;

namespace WatchWord.Models
{
    public class MaterialRequestModel
    {
        public MaterialType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Account Owner { get; set; }

        [ModelBinder(BinderType = typeof(MaterialModelBinder))]
        public List<Word> Words { get; set; }

        public IFormFile Image { get; set; }
    }
}
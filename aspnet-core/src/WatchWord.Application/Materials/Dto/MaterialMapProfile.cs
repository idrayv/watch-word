using AutoMapper;
using WatchWord.Entities;

namespace WatchWord.Materials.Dto
{
    public class MaterialMapProfile : Profile
    {
        public MaterialMapProfile()
        {
            CreateMap<MaterialDto, Material>();
            CreateMap<MaterialDto, Material>().ForMember(m => m.SubtitleFiles, opt => opt.Ignore());
            CreateMap<MaterialDto, Material>().ForMember(m => m.FavoriteMaterials, opt => opt.Ignore());
        }
    }
}

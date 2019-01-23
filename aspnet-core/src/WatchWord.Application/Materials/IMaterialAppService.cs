using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Application.Services;
using WatchWord.Entities;
using WatchWord.Materials.Dto;

namespace WatchWord.Materials
{
    public interface IMaterialAppService : IApplicationService
    {
        Task<MaterialResponseDto> GetMaterial(long id);

        Task<List<MaterialDto>> GetMaterials(int page, int count);

        Task<long> GetCount();

        Task<List<MaterialDto>> Search(string text);

        Task<SaveMaterialResponseDto> Save(Material material);

        Task Delete(long id);
    }
}

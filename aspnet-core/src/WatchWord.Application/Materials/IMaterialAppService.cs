using System.Threading.Tasks;
using Abp.Application.Services;
using WatchWord.Domain.Entities;
using WatchWord.Materials.Dto;
using System.Collections.Generic;

namespace WatchWord.Materials
{
    public interface IMaterialAppService : IApplicationService
    {
        Task<MaterialResponseDto> GetMaterial(long id);

        Task<List<Material>> GetMaterials(int page, int count);

        Task<long> GetCount();

        Task<List<Material>> Search(string text);

        Task<SaveMaterialResponseDto> Save(Material material);

        Task Delete(long id);
    }
}

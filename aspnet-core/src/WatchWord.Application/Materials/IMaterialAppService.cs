using System.Threading.Tasks;
using Abp.Application.Services;
using WatchWord.Domain.Entities;
using WatchWord.Materials.Dto;

namespace WatchWord.Materials
{
    public interface IMaterialAppService : IApplicationService
    {
        Task<SaveMaterialResponseDto> Save(Material material);

        Task Delete(long id);

        Task<MaterialResponseDto> GetMaterial(long id);
    }
}

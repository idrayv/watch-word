using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WatchWord.Authorization;

namespace WatchWord
{
    [DependsOn(
        typeof(WatchWordCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class WatchWordApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<WatchWordAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(WatchWordApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}

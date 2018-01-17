using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WatchWord.Configuration;

namespace WatchWord.Web.Host.Startup
{
    [DependsOn(
       typeof(WatchWordWebCoreModule))]
    public class WatchWordWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public WatchWordWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WatchWordWebHostModule).GetAssembly());
        }
    }
}

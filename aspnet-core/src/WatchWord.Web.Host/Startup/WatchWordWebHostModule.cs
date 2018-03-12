using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Dependency;
using WatchWord.Configuration;
using WatchWord.Pictures;
using WatchWord.ScanWord;
using WatchWord.Infrastructure;

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
            IocManager.Register<IPictureService, PictureService>(DependencyLifeStyle.Transient);
            IocManager.Register<IScanWordParser, ScanWordParser>(DependencyLifeStyle.Transient);
            IocManager.Register<WatchWordProxy, WatchWordProxy>(DependencyLifeStyle.Transient);
        }
    }
}

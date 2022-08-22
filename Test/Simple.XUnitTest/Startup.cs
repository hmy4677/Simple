using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Simple.XUnitTest;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // 注册远程服务
        services.AddRemoteRequest();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
}

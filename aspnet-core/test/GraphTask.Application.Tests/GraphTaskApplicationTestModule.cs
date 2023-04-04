using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace GraphTask;

[DependsOn(
    typeof(GraphTaskApplicationModule),
    typeof(GraphTaskDomainTestModule),
    typeof(GraphTaskApplicationContractsModule)
    )]
public class GraphTaskApplicationTestModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
    }

}

using GraphTask.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace GraphTask;

[DependsOn(
    typeof(GraphTaskEntityFrameworkCoreTestModule)
    )]
public class GraphTaskDomainTestModule : AbpModule
{

}

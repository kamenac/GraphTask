using GraphTask.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace GraphTask.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(GraphTaskEntityFrameworkCoreModule),
    typeof(GraphTaskApplicationContractsModule)
    )]
public class GraphTaskDbMigratorModule : AbpModule
{

}

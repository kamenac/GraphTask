using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace GraphTask.Data;

/* This is used if database provider does't define
 * IGraphTaskDbSchemaMigrator implementation.
 */
public class NullGraphTaskDbSchemaMigrator : IGraphTaskDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

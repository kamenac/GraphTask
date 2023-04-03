using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GraphTask.Data;
using Volo.Abp.DependencyInjection;

namespace GraphTask.EntityFrameworkCore;

public class EntityFrameworkCoreGraphTaskDbSchemaMigrator
    : IGraphTaskDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreGraphTaskDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the GraphTaskDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<GraphTaskDbContext>()
            .Database
            .MigrateAsync();
    }
}

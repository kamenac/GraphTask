﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;
using Volo.Abp;
using Microsoft.EntityFrameworkCore;

namespace GraphTask.EntityFrameworkCore;

[DependsOn(
    typeof(GraphTaskDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
public class GraphTaskEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        GraphTaskEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<GraphTaskDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also GraphTaskMigrationsDbContextFactory for EF Core tooling. */
            options.UseSqlServer();
        });

    }

    public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
    {
        base.OnPostApplicationInitialization(context);

        var dbContextProvider = context.ServiceProvider.GetRequiredService<IDbContextProvider<GraphTaskDbContext>>();
        var unitOfWorkManager = context.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

        using (var unitOfWork = unitOfWorkManager.Begin())
        {
            var dbContext = dbContextProvider.GetDbContext();

            //Removes actual connection as it has been enlisted in a non needed transaction for migration
            dbContext.Database.CloseConnection();
            dbContext.Database.Migrate();
        }
    }

    
}


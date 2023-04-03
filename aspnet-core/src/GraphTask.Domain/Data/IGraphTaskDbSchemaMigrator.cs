using System.Threading.Tasks;

namespace GraphTask.Data;

public interface IGraphTaskDbSchemaMigrator
{
    Task MigrateAsync();
}

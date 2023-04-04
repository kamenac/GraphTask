using EFCore.BulkExtensions;
using GraphTask.EntityFrameworkCore;
using GraphTask.Graph;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace GraphTask.Edge
{
    public class EdgeRepository : EfCoreRepository<GraphTaskDbContext, Graph.Edge, int>, IEdgeRepository
    {
        public EdgeRepository(IDbContextProvider<GraphTaskDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }

        public async Task<List<Tuple<int, int>>> GetListByGraphIdAsync(int graphId)
        {
            var dbContext = await GetDbContextAsync();
            dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

            return dbContext.Edges.Where(x => x.GraphId == graphId).Select(x => new Tuple<int, int>(x.StartNode, x.EndNode)).ToList();
        }

        public async Task InsertManyAsync(IList<Graph.Edge> edges)
        {
            var dbContext = await GetDbContextAsync();

            await dbContext.BulkInsertAsync(edges);  // insert using bulk extensions
        }
    }
}
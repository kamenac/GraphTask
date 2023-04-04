using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace GraphTask.Graph
{
    public interface IEdgeRepository : IRepository<Edge, int>
    {
        Task<List<Tuple<int, int>>> GetListByGraphIdAsync(int graphId);

        Task InsertManyAsync(IList<Edge> edges);
    }

}


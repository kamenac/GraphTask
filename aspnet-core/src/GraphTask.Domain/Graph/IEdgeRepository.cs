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
        /// <summary>
        /// Returns a list of Edge nodes for a given graph id
        /// </summary>
        /// <param name="graphId"></param>
        /// <returns>List of Tuples with start node and end node</returns>
        Task<List<Tuple<int, int>>> GetListByGraphIdAsync(int graphId);

        /// <summary>
        /// Inserts the list of Edges into database
        /// Used to speedup the insert process, instead of relying on the EF dependency tracking
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        Task InsertManyAsync(IList<Edge> edges);
    }

}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace GraphTask.Graph
{
    [ExposeServices(typeof(IGraphAppService), typeof(GraphAppService))]
    public class GraphAppService : CrudAppService<
       Graph,
       GraphDto,
       int,
       PagedAndSortedResultRequestDto,
       CreateGraphDto>, IGraphAppService
    {
        public GraphAppService(
            IRepository<Graph, int> _graphRepository,
            IRepository<Edge, int> _edgeRepository)
        : base(_graphRepository)
        {
            edgeRepository = _edgeRepository;
        }

        private readonly IRepository<Edge, int> edgeRepository;


        private List<string> ConvertFileToEdgeStringInputList(IRemoteStreamContent fileStream)
        {
            StreamReader reader = new StreamReader(fileStream.GetStream());
            string inputData = reader.ReadToEnd();

            return inputData.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public async Task<int> CreateGraphAsync(CreateGraphDto input)
        {
            try
            {
                var graph = new Graph(input.Name);

                var inserted = await this.Repository.InsertAsync(graph, autoSave: true);

                foreach (var inputString in ConvertFileToEdgeStringInputList(input.Content))
                {
                    graph.AddEdge(inputString);
                }

                await edgeRepository.InsertManyAsync(graph.Edges);

                return inserted.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
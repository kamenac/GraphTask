﻿using Abp.UI;
using Microsoft.AspNetCore.Http;
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
using Volo.Abp.Identity;
using Volo.Abp.Uow;

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
        private readonly IEdgeRepository edgeRepository;

        private readonly IUnitOfWorkManager unitOfWorkManager;

        public GraphAppService(
            IRepository<Graph, int> _graphRepository,
            IEdgeRepository _edgeRepository,
            IUnitOfWorkManager _unitOfWorkManager
        )
        : base(_graphRepository)
        {
            edgeRepository = _edgeRepository;
            unitOfWorkManager = _unitOfWorkManager;
        }

        /// <summary>
        /// converts an input file into list of strings, split by new-line character
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns>list of strings</returns>
        private List<string> ConvertFileToStringList(IRemoteStreamContent fileStream)
        {
            using var reader = new StreamReader(fileStream.GetStream());
            string inputData = reader.ReadToEnd();

            return inputData.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task<int> CreateWholeGraphAsync(CreateGraphDto input)
        {
            try
            {
                Graph inserted;

                using (var uow = unitOfWorkManager.Begin()) // use short unit of work, to avoid tracking of the graph edges by EF
                {
                    var graph = new Graph(input.Name);

                    inserted = await this.Repository.InsertAsync(graph, autoSave: true);
                }

                foreach (var inputString in ConvertFileToStringList(input.Content))
                {
                    inserted.AddEdge(inputString);
                }

                await edgeRepository.InsertManyAsync(inserted.Edges);

                using (var uow = unitOfWorkManager.Begin()) // update the graph in db with the computed values
                {
                    var graph = await this.Repository.GetAsync(inserted.Id, includeDetails: false);

                    graph.AverageNumberOfAdjacentNodes = inserted.GetAverageNumberOfAdjacentNodes();
                    graph.NumberOfNodes = inserted.GetNumberOfNodes();
                }

                return inserted.Id;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Could not save graph to database.", ex);
            }
        }

        public async Task<GraphDto> GetWholeGraphAsync(int id)
        {
            //var graphQuery = await this.Repository.GetQueryableAsync();
            //var graph = graphQuery.Where(x => x.Id == id).FirstOrDefault();

            // todo: ^^ find out how the new ABP repository returns related items

            var graph = await Repository.GetAsync(id);
            var edgeTuples = await edgeRepository.GetListByGraphIdAsync(id); // constructing the DTO in steps, for speed reasons

            var dto = this.MapToGetOutputDto(graph);

            foreach (var tuple in edgeTuples)
            {
                dto.Edges.Add(new EdgeDto { Start = tuple.Item1, End = tuple.Item2 });

                if (!dto.Nodes.Contains(tuple.Item1))
                {
                    dto.Nodes.Add(tuple.Item1);
                }

                if (!dto.Nodes.Contains(tuple.Item2))
                {
                    dto.Nodes.Add(tuple.Item2);
                }
            }

            return dto;
        }
    }
}
using GraphTask.Graph;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Xunit;
using Shouldly;

namespace GraphTask
{
    public class GraphTests : GraphTaskDomainTestBase
    {
        private CreateGraphDto PrepareCreateGraphDto(string name, string nodeListData)
        {
            using var memStream = new MemoryStream();
            memStream.ReadAsync(Encoding.UTF8.GetBytes(nodeListData));
            memStream.Position = 0;


            var remoteStream = new RemoteStreamContent(memStream, "", "application/octet-stream");

            var dataset = new CreateGraphDto() { Name = name, Content = remoteStream };

            return dataset;
        }

        [Fact]
        public async Task CreateDatasetTest()
        {
            // IGraphAppService graphServiceq = GetService<IGraphAppService>();
            IGraphAppService graphService = GetRequiredService<IGraphAppService>();
            var createDatasetDto = PrepareCreateGraphDto("first dataset", "0 42");

            var id = await graphService.CreateWholeGraphAsync(createDatasetDto);
        }


        [Fact]
        public async Task AddEdgesAndNodesTest()
        {
            // prepare
            var graph = new Graph.Graph("test graph");

            // act
            graph.AddEdge("0 42");

            // assert

            graph.Edges.Count.ShouldBe(1);

            var numberOfNodes = graph.GetNumberOfNodes();
            numberOfNodes.ShouldBe(2);

            graph.NodeList.ShouldContain(0); // should contain node 0

            graph.NodeList.ShouldContain(42); // should contain node 42
        }


        [Fact]
        public async Task GraphOperationsTest()
        {
            // prepare
            var graph = new Graph.Graph("test graph");

            // act
            graph.AddEdge("0 1");
            graph.AddEdge("0 3");
            graph.AddEdge("1 2");

            var adjacentNodeCountForNodeZero = graph.GetNumberOfAdjacentNodes(0);
            var adjacentNodeCountForNodeOne = graph.GetNumberOfAdjacentNodes(1);
            var adjacentNodeCountForNodeTwo = graph.GetNumberOfAdjacentNodes(2);
            var adjacentNodeCountForNodeThree = graph.GetNumberOfAdjacentNodes(3);

            // assert

            adjacentNodeCountForNodeZero.ShouldBe(2);
            adjacentNodeCountForNodeOne.ShouldBe(2);
            adjacentNodeCountForNodeTwo.ShouldBe(1);
            adjacentNodeCountForNodeThree.ShouldBe(1);
        }
    }
}
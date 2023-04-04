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
    }
}
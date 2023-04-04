using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.Application.Dtos;

namespace GraphTask.Graph
{
    public class GraphDto : AuditedEntityDto<int>
    {
        public string Name { get; set; }

        public double AverageNumberOfAdjacentNodes { get; set; }

        public int NumberOfNodes { get; set; }

        public List<EdgeDto> Edges { get; set; } = new List<EdgeDto>();

        public List<int> Nodes { get; set; } = new List<int>();

    }
}

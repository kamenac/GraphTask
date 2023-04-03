using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace GraphTask.Graph
{
    public class Edge : Entity<int>
    {
        public Edge(int startNode, int endNode)
        {
            StartNode = startNode;
            EndNode = endNode;
        }

        public int EndNode { get; set; }

        public int StartNode { get; set; }

        public bool HasNode(int vertex)
        {
            return (StartNode == vertex || EndNode == vertex);
        }
    }
}
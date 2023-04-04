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

        public int GraphId { get; set; }

        /// <summary>
        /// Does the edge contains a node? Either start node or end node
        /// </summary>
        /// <param name="node"></param>
        /// <returns>true if it contains a node, false otherwise</returns>
        public bool ContainsNode(int node)
        {
            return (StartNode == node || EndNode == node);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace GraphTask.Graph
{
    public class EdgeDto
    {
        public int End { get; set; }

        public int Start { get; set; }

        /// <summary>
        /// Does the edge contains a node? Either start node or end node
        /// </summary>
        /// <param name="node"></param>
        /// <returns>true if it contains a node, false otherwise</returns>
        public bool ContainsNode(int node)
        {
            return (Start == node || End == node);
        }
    }
}

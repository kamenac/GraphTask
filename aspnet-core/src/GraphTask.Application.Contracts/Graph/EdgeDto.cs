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

        public bool ContainsNode(int node)
        {
            return (Start == node || End == node);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace GraphTask.Graph
{
    public class EdgeDto
    {
        public int EndNode { get; set; }

        public int StartNode { get; set; }

        public bool ContainsNode(int node)
        {
            return (StartNode == node || EndNode == node);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace GraphTask.Graph
{
    public class Graph : AuditedAggregateRoot<int>
    {
        private Graph()
        {
            Edges = new List<Edge>();
            Nodes = new List<int>();
        }

        public Graph(string name) : this()
        {
            Name = name;
        }

        private List<int> Nodes { get; set; }

        public double AverageNumberOfAdjacentNodes { get; set; }

        public int NumberOfNodes { get; set; }

        public List<Edge> Edges { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        private Tuple<int, int> ParseEdgeString(string input)
        {
            var parts = input.Split(" ");

            int startId = int.Parse(parts[1]);
            int endId = int.Parse(parts[0]);

            return new Tuple<int, int>(startId, endId);
        }

        public void AddEdge(string input)
        {
            var parts = ParseEdgeString(input);
            AddEdge(parts.Item1, parts.Item2);
        }

        public void AddEdge(int from, int to)
        {
            Edges.Add(new Edge(from, to) { GraphId = this.Id }); // need to manually set GraphId, we don't track dependency on the Graph object

            if (!Nodes.Contains(from))
            {
                Nodes.Add(from);
            }

            if (!Nodes.Contains(to))
            {
                Nodes.Add(to);
            }
        }

        public double GetAverageNumberOfAdjacentNodes()
        {
            var countList = new List<int>();

            foreach (var node in Nodes)
            {
                countList.Add(GetNumberOfAdjacentNodes(node));
            }

            return countList.Average();
        }

        public int GetNumberOfAdjacentNodes(int fromNode)
        {
            int count = 0;

            foreach (var edge in Edges)
            {
                if (edge.ContainsNode(fromNode))
                {
                    count++;
                }
            }

            return count;
        }

        public int GetNumberOfNodes()
        {
            return Nodes.Count;
        }
    }
}
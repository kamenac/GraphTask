using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// Average number of adjacent nodes in the graph
        /// </summary>
        public double AverageNumberOfAdjacentNodes { get; set; }

        /// <summary>
        /// List of Edges
        /// </summary>
        public List<Edge> Edges { get; set; } // todo: make private and expose read-only list, similar to NodeList.
                                              // make sure EF can still access it https://learn.microsoft.com/en-us/ef/core/modeling/backing-field?tabs=data-annotations


        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// Read-only list of nodes.
        /// Gets updated when new edge is added
        /// </summary>
        [NotMapped]
        public ReadOnlyCollection<int> NodeList
        {
            get
            {
                return Nodes.AsReadOnly();
            }
        }

        /// <summary>
        /// Number of nodes in the graph
        /// </summary>
        public int NumberOfNodes { get; set; }

        /// <summary>
        /// Splits the edge definition string into two integers and returns them as a Tuple
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Tuple with start and end node</returns>
        private Tuple<int, int> ParseEdgeString(string input)
        {
            var parts = input.Split(" ");

            int startId = int.Parse(parts[1]);
            int endId = int.Parse(parts[0]);

            return new Tuple<int, int>(startId, endId);
        }

        /// <summary>
        /// Adds edge to graph and its nodes to node list, if they are not already there
        /// </summary>
        /// <param name="input"></param>
        public void AddEdge(string input)
        {
            var parts = ParseEdgeString(input);
            AddEdge(parts.Item1, parts.Item2);
        }

        /// <summary>
        /// Adds edge to graph and its nodes to node list, if they are not already there
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
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

        /// <summary>
        /// Returns an average number of nodes directly adjacent to each other
        /// </summary>
        /// <returns></returns>
        public double GetAverageNumberOfAdjacentNodes()
        {
            var countList = new List<int>();

            foreach (var node in Nodes)
            {
                countList.Add(GetNumberOfAdjacentNodes(node));
            }

            return countList.Average();
        }

        /// <summary>
        /// Returns a number of the nodes directly adjacent to the specified node
        /// </summary>
        /// <param name="fromNode"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the number of nodes in the graph
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfNodes()
        {
            return Nodes.Count;
        }
    }
}
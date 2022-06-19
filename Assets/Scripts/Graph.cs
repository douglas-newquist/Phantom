using System.Collections;
using System.Collections.Generic;

namespace Phantom
{
	[System.Serializable]
	public partial class Graph<T> : IEnumerable<Edge<T>>
	{
		public bool Directional { get; private set; }

		private List<T> vertices = new List<T>();

		/// <summary>
		/// Number of vertices in this graph
		/// </summary>
		public int VertexCount => vertices.Count;

		public IEnumerable<T> Vertices => vertices;

		private List<Edge<T>> edges = new List<Edge<T>>();

		public int EdgeCount => edges.Count;

		public IEnumerable<Edge<T>> Edges => edges;

		public Graph() { }

		public Graph(bool directional)
		{
			Directional = directional;
		}

		public override string ToString()
		{
			string s = Directional ? "Directional Graph" : "Graph";

			s += " with " + VertexCount + " vertices and " + edges.Count + " edges";
			return s;
		}

		public void AddEdge(T source, T destination, float weight)
		{
			AddEdge(source, destination, weight, Directional);
		}

		public void AddEdge(T source, T destination, float weight, bool directional)
		{
			AddEdge(new Edge<T>(source, destination, weight, directional));
		}

		public void AddEdge(Edge<T> edge)
		{
			edges.Add(edge);

			if (!vertices.Contains(edge.source))
				vertices.Add(edge.source);
			if (!vertices.Contains(edge.destination))
				vertices.Add(edge.destination);
		}

		public bool RemoveEdge(Edge<T> edge)
		{
			return edges.Remove(edge);
		}

		/// <summary>
		/// Gets all edges connected to the given vertex
		/// </summary>
		public IEnumerable<Edge<T>> GetVertexEdges(T vertex)
		{
			foreach (var edge in edges)
				if (Equals(edge.source, vertex) || Equals(edge.destination, vertex))
					yield return edge;
		}

		/// <summary>
		/// Gets all edges leaving the given vertex
		/// </summary>
		public IEnumerable<Edge<T>> GetEdgesLeaving(T source)
		{
			foreach (var edge in GetVertexEdges(source))
				if (!edge.directional || Equals(edge.source, source))
					yield return edge;
		}

		/// <summary>
		/// Gets all edges entering the given vertex
		/// </summary>
		public IEnumerable<Edge<T>> GetEdgesEntering(T destination)
		{
			foreach (var edge in GetVertexEdges(destination))
				if (!edge.directional || Equals(edge.destination, destination))
					yield return edge;
		}

		/// <summary>
		/// Gets a minimum spanning tree version of this tree
		/// </summary>
		public Graph<T> MST()
		{
			Graph<T> tree = new Graph<T>(Directional);
			edges.Sort();

			foreach (var edge in Edges)
			{
				tree.AddEdge(edge);

				if (tree.ContainsCycle())
					tree.RemoveEdge(edge);
				else if (tree.EdgeCount == VertexCount - 1)
					return tree;
			}

			return tree;
		}

		/// <summary>
		/// Returns true if this graph contains at least 1 cycle
		/// </summary>
		public bool ContainsCycle()
		{
			foreach (var vertex in Vertices)
				if (ContainsCycle(vertex))
					return true;

			return false;
		}

		/// <summary>
		/// Returns true if this graph contains at least 1 cycle
		/// </summary>
		public bool ContainsCycle(T start)
		{
			return ContainsCycle(null, start, new List<T>());
		}

		/// <summary>
		/// Returns true if this graph contains at least 1 cycle
		/// </summary>
		private bool ContainsCycle(Edge<T> previous, T source, List<T> searched)
		{
			foreach (var edge in GetEdgesLeaving(source))
			{
				if (edge == previous) continue;

				if (Equals(edge.source, edge.destination))
					return true;

				T destination = edge.destination;
				if (!edge.directional && Equals(destination, source))
					destination = edge.source;

				if (Equals(destination, source))
					return true;
				if (searched.Contains(destination))
					return true;

				searched.Add(destination);

				if (ContainsCycle(edge, destination, searched))
					return true;
			}

			return false;
		}

		public IEnumerator<Edge<T>> GetEnumerator() => edges.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => edges.GetEnumerator();
	}
}

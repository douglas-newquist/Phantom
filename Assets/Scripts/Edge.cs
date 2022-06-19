using System;

namespace Phantom
{
	[System.Serializable]
	public class Edge<T> : IComparable<Edge<T>>
	{
		public readonly T source, destination;

		public readonly float weight;

		public readonly bool directional;

		public Edge(T source, T destination, float weight, bool directional)
		{
			this.source = source;
			this.destination = destination;
			this.weight = weight;
			this.directional = directional;
		}

		public override string ToString()
		{
			if (directional)
				return "Edge " + source + " -> " + destination + " with weight of " + weight;
			return "Edge " + source + " <-> " + destination + " with weight of " + weight;
		}

		public int CompareTo(Edge<T> other)
		{
			return weight.CompareTo(other.weight);
		}
	}
}

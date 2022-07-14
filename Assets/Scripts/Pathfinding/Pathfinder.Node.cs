using System;

namespace Phantom.Pathfinding
{
	public abstract partial class Pathfinder
	{
		/// <summary>
		/// Stores information about nodes that have been searched
		/// </summary>
		/// <typeparam name="TCell">Coordinate type used in the map</typeparam>
		protected class Node<TCell> : IComparable<Node<TCell>>
		{
			/// <summary>
			/// Best node to reach this one from
			/// </summary>
			public Node<TCell> previous;

			/// <summary>
			/// Where this node is in the map
			/// </summary>
			public TCell pos;

			/// <summary>
			/// Cost of getting to this cell
			/// </summary>
			public float cost;

			public Node(Node<TCell> previous, TCell cell, float cost)
			{
				this.previous = previous;
				this.pos = cell;
				this.cost = cost;
			}

			public int CompareTo(Node<TCell> other)
			{
				return cost.CompareTo(other.cost);
			}
		}
	}
}

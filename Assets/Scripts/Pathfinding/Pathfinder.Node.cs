using System;
using UnityEngine;

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
			private Node<TCell> previous;

			/// <summary>
			/// Best node to reach this one from
			/// </summary>
			public Node<TCell> Previous
			{
				get => previous;
				set => previous = value;
			}

			/// <summary>
			/// Where this node is in the map
			/// </summary>
			private TCell cell;

			/// <summary>
			/// Where this node is in the map
			/// </summary>
			public TCell Cell
			{
				get => cell;
				set => cell = value;
			}

			/// <summary>
			/// Cost of getting to this cell
			/// </summary>
			private float cost;

			/// <summary>
			/// Cost of getting to this cell
			/// </summary>
			public float Cost
			{
				get => cost;
				set => cost = Mathf.Clamp(value, 0, float.MaxValue);
			}

			public Node(Node<TCell> previous, TCell cell, float cost)
			{
				Previous = previous;
				Cell = cell;
				Cost = cost;
			}

			public int CompareTo(Node<TCell> other)
			{
				return Cost.CompareTo(other.Cost);
			}
		}
	}
}

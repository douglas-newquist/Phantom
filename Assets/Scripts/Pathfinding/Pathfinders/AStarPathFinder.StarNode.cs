using System;
using UnityEngine;

namespace Phantom.Pathfinding
{
	public partial class AStarPathFinder
	{
		protected class StarNode<T> : Node<T>, IComparable<StarNode<T>>
		{
			/// <summary>
			/// The cost of getting to this cell
			/// </summary>
			public float GScore
			{
				get => Cost;
				set => Cost = value;
			}

			private float hScore = 0;

			/// <summary>
			/// Estimated remaining cost to reach the goal
			/// </summary>
			public float HScore
			{
				get => hScore;
				set => hScore = Mathf.Clamp(value, 0, float.MaxValue);
			}

			/// <summary>
			/// Estimated total cost to reach the goal
			/// </summary>
			public float FScore => GScore + HScore;

			public StarNode(T cell) : base(cell) { }

			public override string ToString()
			{
				var s = Cell + "\tG:" + Cost + "\tH:" + HScore + "\tF:" + FScore;
				if (Previous != null)
					s += " from " + Previous.Cell;
				return s;
			}

			public int CompareTo(StarNode<T> other)
			{
				return FScore.CompareTo(other.FScore);
			}
		}
	}
}

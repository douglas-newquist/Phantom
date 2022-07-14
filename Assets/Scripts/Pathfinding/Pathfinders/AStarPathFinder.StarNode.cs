using System;

namespace Phantom.Pathfinding
{
	public partial class AStarPathFinder
	{
		protected class StarNode<T> : Node<T>, IComparable<StarNode<T>>
		{
			public float h;

			public float FScore => cost + h;

			/// <summary>
			///
			/// </summary>
			/// <param name="cell"></param>
			/// <param name="gScore">Actual cost to get here</param>
			/// <param name="hScore">Estimated remaining cost to reach goal</param>
			public StarNode(StarNode<T> previous, T cell, float gScore, float hScore) : base(previous, cell, gScore)
			{
				this.h = hScore;
			}

			public override string ToString()
			{
				var s = pos + "\tG:" + cost + "\tH:" + h + "\tF:" + FScore;
				if (previous != null)
					s += " from " + previous.pos;
				return s;
			}

			public int CompareTo(StarNode<T> other)
			{
				return FScore.CompareTo(other.FScore);
			}
		}
	}
}

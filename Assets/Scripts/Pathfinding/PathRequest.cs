using System;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	public sealed class PathRequest<TMap, TCell>
	{
		/// <summary>
		/// Map to path find on
		/// </summary>
		public TMap Map { get; set; }

		/// <summary>
		/// Agent that is performing path finding
		/// </summary>
		public IPathAgent<TMap, TCell> Agent { get; set; }

		/// <summary>
		/// Collection of starting cells
		/// </summary>
		public IEnumerable<TCell> StartingCells { get; set; }

		public TCell StartingCell
		{
			set => StartingCells = new TCell[1] { value };
		}

		private Func<TMap, TCell, bool> goalReached;

		/// <summary>
		/// Function to check if the goal has been reached
		/// </summary>
		public Func<TMap, TCell, bool> GoalReached
		{
			get => goalReached;
			set => goalReached = value;
		}

		IEnumerable<TCell> goalCells = null;

		/// <summary>
		/// Does this request have known ending points
		/// </summary>
		public bool HasKnownGoalCells => goalCells != null;

		public IEnumerable<TCell> GoalCells
		{
			get => goalCells;
			set
			{
				GoalReached = GoalCellReached;
				goalCells = value;
			}
		}

		public TCell GoalCell
		{
			set => GoalCells = new TCell[1] { value };
		}

		/// <summary>
		/// The resulting path
		/// </summary>
		public Path<TCell> Path { get; private set; }

		public PathRequest()
		{
			GoalReached = GoalCellReached;
			Path = new Path<TCell>();
		}

		public PathRequest(TMap map, IPathAgent<TMap, TCell> agent) : this()
		{
			Map = map;
			Agent = agent;
		}

		public bool GoalCellReached(TMap map, TCell cell)
		{
			foreach (var goal in GoalCells)
				if (Equals(cell, goal))
					return true;

			return false;
		}

		public float GetCheapestGoalCost(TCell start)
		{
			if (!HasKnownGoalCells) return 0;

			float best = float.MaxValue;
			foreach (var goal in GoalCells)
				best = Mathf.Min(best, Agent.GetPathCost(Map, start, goal));

			return best;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	/// <summary>
	/// Defines a sequence of coordinates to reach the desired location
	/// </summary>
	/// <typeparam name="TCell">Coordinate type used in the map</typeparam>
	[System.Serializable]
	public class Path<TCell> : IEnumerable<TCell>
	{
		/// <summary>
		/// When the pathfinder started search
		/// </summary>
		public DateTime Started { get; private set; }

		/// <summary>
		/// When the pathfinder finished or terminated searched
		/// </summary>
		public DateTime Ended { get; private set; }

		/// <summary>
		/// How long the pathfinder took finish
		/// </summary>
		public TimeSpan Duration
		{
			get
			{
				switch (Status)
				{
					case PathStatus.Searching:
						return DateTime.Now - Started;

					default:
						return Ended - Started;
				}
			}
		}

		public PathStatus Status { get; private set; }

		private List<TCell> cells;

		public IEnumerable<TCell> Cells => cells;

		private int waypointNumber = 0;

		public bool Finished => Status != PathStatus.Searching;

		public int Length => cells.Count;

		public Path(PathStatus status, List<TCell> path) : this()
		{
			SetPath(path, status);
		}

		public Path()
		{
			Started = DateTime.Now;
			Status = PathStatus.Searching;
		}

		public override string ToString()
		{
			string s = "Path with status " + Status + " which took " + Duration.TotalSeconds + " seconds";

			if (cells != null)
				s += " of length " + cells.Count;

			return s;
		}

		public TCell this[int i]
		{
			get => cells[i];
			set => cells[i] = value;
		}

		public void NextWaypoint()
		{
			waypointNumber++;
		}

		/// <summary>
		/// Marks this path as completed
		/// </summary>
		/// <param name="path">Found path, if any</param>
		/// <param name="status">Final status</param>
		public void SetPath(List<TCell> path, PathStatus status)
		{
			Ended = DateTime.Now;
			cells = path;
			Status = status;
		}

		public IEnumerator<TCell> GetEnumerator()
		{
			return ((IEnumerable<TCell>)cells).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)cells).GetEnumerator();
		}

		public bool TryGetWaypoint(out TCell cell)
		{
			switch (Status)
			{
				case PathStatus.Found:
				case PathStatus.TimedOut:
					if (waypointNumber < cells.Count)
					{
						cell = cells[waypointNumber];
						return true;
					}
					break;
			}

			cell = default(TCell);
			return false;
		}

		public void RemoveAt(int index)
		{
			cells.RemoveAt(index);
		}

		public void Reset()
		{
			waypointNumber = 0;
		}

		public void Reverse()
		{
			cells.Reverse();
		}

		public void DrawGizmos(Func<TCell, Vector3> cellToWorld, float radius)
		{
			var _color = Gizmos.color;

			for (int i = 0; i < Length; i++)
			{
				if (i < waypointNumber)
					Gizmos.color = Color.green;
				else if (i == waypointNumber)
					Gizmos.color = Color.yellow;
				else
					Gizmos.color = Color.white;

				var start = cellToWorld(this[i]);
				Gizmos.DrawWireSphere(start, radius);

				if (i > 0)
				{
					var end = cellToWorld(this[i - 1]);
					Gizmos.DrawLine(start, end);
				}
			}

			Gizmos.color = _color;
		}
	}
}

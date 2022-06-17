using System;
using System.Collections;
using System.Collections.Generic;

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

		public List<TCell> Cells;

		public bool Finished => Status != PathStatus.Searching;

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

			if (Cells != null)
				s += " of length " + Cells.Count;

			return s;
		}

		/// <summary>
		/// Marks this path as completed
		/// </summary>
		/// <param name="path">Found path, if any</param>
		/// <param name="status">Final status</param>
		public void SetPath(List<TCell> path, PathStatus status)
		{
			Ended = DateTime.Now;
			Cells = path;
			Status = status;
		}

		public IEnumerator<TCell> GetEnumerator()
		{
			return ((IEnumerable<TCell>)Cells).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)Cells).GetEnumerator();
		}
	}
}

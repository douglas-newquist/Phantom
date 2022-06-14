using System;
using System.Collections;
using System.Collections.Generic;

namespace Phantom.Pathfinding
{
	[System.Serializable]
	public class Path<TCell> : IEnumerable<TCell>
	{
		public DateTime Started { get; private set; }

		public DateTime Ended { get; private set; }

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

		public Path(PathStatus status, List<TCell> path)
		{
			Started = DateTime.Now;
			Status = status;
			Cells = path;
		}

		public Path() : this(PathStatus.Searching, null) { }

		public override string ToString()
		{
			if (Cells != null && Cells.Count > 0)
				return "Path of length " + Cells.Count + " with status " + Status + " which took " + Duration.TotalSeconds + " seconds";

			return "Path status " + Status + " which took " + Duration.TotalSeconds + " seconds";
		}

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

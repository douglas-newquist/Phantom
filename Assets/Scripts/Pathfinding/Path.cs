using System.Collections.Generic;

namespace Phantom.Pathfinding
{
	[System.Serializable]
	public class Path<TCell>
	{
		public PathStatus Status { get; private set; }

		public List<TCell> Cells { get; private set; }

		public bool Finished => Status != PathStatus.Searching;

		public Path(PathStatus status, List<TCell> path)
		{
			Status = status;
			Cells = path;
		}

		public Path() : this(PathStatus.Searching, null) { }

		public void SetPath(List<TCell> path, PathStatus status)
		{
			Cells = path;
			Status = status;
		}
	}
}

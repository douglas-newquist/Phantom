using UnityEngine;
using Phantom.Pathfinding;

namespace Phantom
{
	[System.Serializable]
	public class VertexPathSeeker
	{
		[SerializeField]
		private VertexPathAgent pathAgent;

		public VertexPathAgent PathAgent
		{
			get => pathAgent;
			set
			{
				if (value == null)
					throw new System.ArgumentNullException("PathAgent");
				pathAgent = value;
			}
		}

		public Level Level => GameManager.CurrentLevel;

		public Grid2D<int> Vertices => Level.TileLayerMap.VertexTiles.Vertices;

		[SerializeField]
		[Range(0.1f, Level.TileSize)]
		private float followTolerance = Level.TileSize / 4;

		/// <summary>
		/// How close does the entity have to be to a waypoint to mark it as reached
		/// </summary>
		public float FollowTolerance
		{
			get => followTolerance;
			set => followTolerance = Mathf.Clamp(value, float.Epsilon, Level.TileSize);
		}

		[SerializeField]
		private LayerMask mask;

		private Path<Vector2Int> path;

		private bool followingPath = false;

		private Vector3 lastPathTarget = Vector3.positiveInfinity;

		/// <summary>
		/// Converts map coordinates to world coordinates
		/// </summary>
		public Vector3 MapToWorldCell(Vector2Int cell)
		{
			return Level.GridToWorldPoint((Vector3Int)cell);
		}

		/// <summary>
		/// Do we need to use the path finder
		/// </summary>
		/// <param name="current">Current position</param>
		/// <param name="target">Target position</param>
		private bool NeedsPathFinder(Vector2 current, Vector2 target)
		{
			Vector2 delta = target - current;
			var hit = Physics2D.Raycast(current, delta.normalized, delta.magnitude, mask);
			return hit.transform != null;
		}

		/// <summary>
		/// Converts world coordinates to map coordinates
		/// </summary>
		public Vector2Int WorldToMapCell(Vector3 position)
		{
			return (Vector2Int)Level.GetClosestVertex(position);
		}

		/// <summary>
		/// Gets the next waypoint to reach the desired target
		/// </summary>
		/// <param name="current">Current location</param>
		/// <param name="target">Desired location</param>
		/// <returns>Coordinates to move to</returns>
		public Vector3 GetWaypoint(Vector3 current, Vector3 target)
		{
			if (NeedsPathFinder(current, target) == false)
			{
				followingPath = false;
				return target;
			}

			if (!followingPath || lastPathTarget != target)
			{
				var request = new PathRequest<IGrid2D<int>, Vector2Int>()
				{
					Map = Vertices,
					Agent = pathAgent,
					StartingCell = WorldToMapCell(current),
					GoalCell = WorldToMapCell(target)
				};

				pathAgent.FindPath(request);
				path = request.Path;
				lastPathTarget = target;
				followingPath = true;
			}

			if (path.TryGetWaypoint(out var waypoint))
			{
				var position = MapToWorldCell(waypoint);
				if (Vector3.Distance(current, position) < FollowTolerance)
					path.NextWaypoint();
			}

			if (path.TryGetWaypoint(out waypoint))
				return MapToWorldCell(waypoint);

			return current;
		}

		public void DrawGizmos()
		{
			if (followingPath)
				switch (path.Status)
				{
					case PathStatus.Found:
					case PathStatus.TimedOut:
						path.DrawGizmos(MapToWorldCell, FollowTolerance);
						break;
				}
		}
	}
}

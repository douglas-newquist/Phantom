using Phantom.Pathfinding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Connect Areas")]
	public class ConnectAreasGridGen : GridGen
	{
		public int findAreasWith = 0;

		public int connectWith;

		public VertexPathAgent pathAgent;

		[Range(1, 16)]
		public float pathRadius = 2;

		[MinMax(0, 1)]
		public FloatRange connectivity = 0.9f;

		public override Grid2D<int> ApplyOnce(Grid2D<int> design, RectInt area)
		{
			design = new Grid2D<int>(design);

			var rooms = design.FloodFindGroups(area, (a, b) => a == b);

			rooms = rooms.Where((room) => design.Get(room[0].x, room[0].x) == findAreasWith).ToList();

			var connections = connectivity.Random;

			for (int i = 0; i < rooms.Count; i++)
			{
				for (int j = i + 1; j < rooms.Count; j++)
				{
					if (Random.Range(0f, 1f) < connections)
						ConnectRooms(design, rooms[i], rooms[j]);
				}
			}

			return design;
		}

		protected void ConnectRooms(Grid2D<int> design, List<Vector2Int> room1, List<Vector2Int> room2)
		{
			var start = FindClosestPoint(room1, room2);
			var end = FindClosestPoint(room2, room1);

			var path = pathAgent.FindPath(design, start, end);

			Debug.Log(path);

			if (path.Status == PathStatus.Found)
				PlacePath(design, path);
		}

		public Vector2 GetRoomCenter(List<Vector2Int> room)
		{
			var center = Vector2.zero;

			if (room.Count == 0)
				return center;

			foreach (var cell in room)
				center += center;

			return center / room.Count;
		}

		protected Vector2Int FindClosestPoint(List<Vector2Int> room1, List<Vector2Int> room2)
		{
			var center = GetRoomCenter(room2);
			var best = room1[0];
			var bestDist = Vector2.Distance(center, best);

			foreach (var cell in room1)
			{
				var dist = Vector2.Distance(center, cell);
				if (dist < bestDist)
				{
					best = cell;
					bestDist = dist;
				}
			}

			return best;
		}

		protected void PlacePath(Grid2D<int> design, Path<Vector2Int> path)
		{
			foreach (var point in path)
			{
				design.Set(point.x, point.y, connectWith);
			}
		}
	}
}

using Phantom.Pathfinding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Connect Areas")]
	public class ConnectAreasGridGen : GridGen
	{
		public class Room
		{
			public List<Vector2Int> cells;

			public Vector2 center;

			public Room(List<Vector2Int> cells)
			{
				this.cells = cells;

				foreach (var cell in cells)
					center += cell;

				center /= cells.Count;
			}

			public Vector2Int ClosestCellToPoint(Vector2 point)
			{
				var best = cells[0];
				var bestDist = Vector2.Distance(point, best);

				foreach (var cell in cells)
				{
					var dist = Vector2.Distance(point, cell);
					if (dist < bestDist)
					{
						best = cell;
						bestDist = dist;
					}
				}

				return best;
			}

			public Vector2Int FindClosestCellToRoom(Room other)
			{
				return ClosestCellToPoint(other.center);
			}
		}

		public int findAreasWith = 0;

		public int connectWith;

		[MinMax(0, 8)]
		public IntRange connections = new IntRange(1, 2);

		public VertexPathAgent pathAgent;

		[Range(1, 16)]
		public float pathRadius = 2;

		public override Grid2D<int> ApplyOnce(Grid2D<int> design, RectInt area)
		{
			design = new Grid2D<int>(design);

			var rooms = GetRooms(design, area);

			if (rooms.Count < 2)
				return design;

			for (int i = 0; i < rooms.Count; i++)
			{
				for (int c = connections.Random; c > 0; c--)
				{
					int j = PickTargetRoom(rooms, i);
					Debug.Log("Connecting room " + i + " to room " + j);
					ConnectRooms(design, rooms[i], rooms[j]);
				}
				return design;
			}

			return design;
		}

		protected List<Room> GetRooms(Grid2D<int> design, RectInt area)
		{
			var rooms = new List<Room>();

			foreach (var region in design.FloodFindGroups(area, (a, b) => a == b))
				if (design.Get(region[0].x, region[0].y) == findAreasWith)
					rooms.Add(new Room(region));

			return rooms;
		}

		protected int PickTargetRoom(List<Room> rooms, int start)
		{
			int end;
			{
				end = Random.Range(0, rooms.Count);
			} while (end == start) ;

			return end;
		}

		protected void ConnectRooms(Grid2D<int> design, Room room1, Room room2)
		{
			var start = room1.FindClosestCellToRoom(room2);
			var end = room2.FindClosestCellToRoom(room1);

			var path = pathAgent.FindPath(design, start, end);

			if (path.Status == PathStatus.Found)
				PlacePath(design, path);
		}

		protected void PlacePath(Grid2D<int> design, Path<Vector2Int> path)
		{
			foreach (var point in path)
			{
				design.Set(point, connectWith);
			}
		}
	}
}

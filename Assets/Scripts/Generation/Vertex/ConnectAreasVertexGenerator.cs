using Phantom.Pathfinding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Connect Areas")]
	public sealed class ConnectAreasVertexGenerator : VertexGenerator
	{
		public class Room
		{
			public int id;

			public List<Vector2Int> cells;

			public Vector2 center;

			public Room(List<Vector2Int> cells, int id)
			{
				this.cells = cells;
				this.id = id;

				foreach (var cell in cells)
					center += cell;

				center /= cells.Count;
			}

			public override string ToString()
			{
				return "Room " + id + " at " + center;
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

			public float Distance(Room other)
			{
				return Vector2.Distance(center, other.center);
			}
		}

		public int findAreasWith = 0;

		public int connectWith;

		[MinMax(0, 8)]
		public IntRange extraConnections = new IntRange(1, 2);

		public VertexPathAgent pathAgent;

		[Range(0, 16)]
		public float pathRadius = 2;

		protected override VertexTileMap ApplyOnce(VertexTileMap design, RectInt area)
		{
			design = new VertexTileMap(design);

			var rooms = GetRooms(design, area);

			if (rooms.Count < 2)
				return design;

			var graph = new Graph<Room>();

			foreach (var room1 in rooms)
				foreach (var room2 in rooms)
					if (room1 != room2)
						graph.AddEdge(room1, room2, room1.Distance(room2));

			foreach (var edge in graph.MST())
			{
				ConnectRooms(design, edge.source, edge.destination);
			}

			foreach (var room in rooms)
			{
				for (int extra = extraConnections.Random; extra > 0; extra--)
				{
					var room2 = rooms[Random.Range(0, rooms.Count)];
					ConnectRooms(design, room, room2);
				}
			}

			return design;
		}

		private List<Room> GetRooms(VertexTileMap design, RectInt area)
		{
			var rooms = new List<Room>();

			foreach (var region in design.Vertices.FloodFindGroups(area, (a, b) => a == b))
				if (design.Vertices.Get(region[0].x, region[0].y) == findAreasWith)
					rooms.Add(new Room(region, rooms.Count));

			return rooms;
		}

		private int PickTargetRoom(List<Room> rooms, int start)
		{
			int end;
			{
				end = Random.Range(0, rooms.Count);
			} while (end == start) ;

			return end;
		}

		private void ConnectRooms(VertexTileMap design, Room room1, Room room2)
		{
			var request = new PathRequest<IGrid2D<int>, Vector2Int>()
			{
				Map = design.Vertices,
				Agent = pathAgent,
				StartingCell = room1.FindClosestCellToRoom(room2),
				GoalCell = room2.FindClosestCellToRoom(room1)
			};

			var path = pathAgent.FindPath(request);

			if (request.Path.Status == PathStatus.Found)
				PlacePath(design, request.Path);
		}

		private void PlacePath(VertexTileMap design, Path<Vector2Int> path)
		{
			foreach (var point in path)
			{
				for (int xi = -(int)pathRadius; xi <= pathRadius; xi++)
				{
					for (int yi = -(int)pathRadius; yi <= pathRadius; yi++)
					{
						var p = new Vector2Int(point.x + xi, point.y + yi);
						if (Vector2.Distance(point, p) <= pathRadius
							&& design.Vertices.InBounds(p.x, p.y))
							design.Vertices.Set(p, connectWith);
					}
				}
			}
		}
	}
}

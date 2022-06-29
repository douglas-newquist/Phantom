using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Phantom.Pathfinding;
using Phantom.ObjectPooling;
using UnityEngine.Tilemaps;

namespace Phantom
{
	public class Tester : MonoBehaviour
	{
		public ShipGenerator shipGenerator;
		public ShipBuilder shipBuilder;
		public LevelGenerator mapGenerator;
		public LevelBuilder levelBuilder;


		public ShipDesign shipDesign;
		public LevelDesign levelDesign;

		public SimpleFollowCameraExtension followCameraExtension;

		public Transform start, end;
		public VertexPathAgent pathAgent;
		public Path<Vector2Int> path;


		// Start is called before the first frame update
		void Start()
		{
			levelDesign = mapGenerator.Create();
			GameManager.CurrentLevel = levelBuilder.Create(levelDesign).GetComponent<Level>();

			shipDesign = shipGenerator.Create(32, 32);
			var shipName = shipBuilder.CreateRegister(shipDesign);
			var ship = ObjectPool.Spawn(shipName, new PositionSpawner(GameManager.CurrentLevel.WorldBounds));

			followCameraExtension.SetTarget(ship);
		}

		private void Update()
		{
			var s = new Vector2Int((int)start.position.x, (int)start.position.y);
			var e = new Vector2Int((int)end.position.x, (int)end.position.y);

			path = pathAgent.FindPath(GameManager.CurrentLevel.Vertices, s, e);
		}

		private void OnDrawGizmos()
		{
			if (path.Status == PathStatus.Found)
				foreach (var cell in path)
				{
					var pos = new Vector3(cell.x, cell.y, 0);
					Gizmos.DrawWireSphere(pos, 0.5f);
				}
		}
	}
}

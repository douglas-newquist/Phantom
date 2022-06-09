using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Phantom.Pathfinding;

namespace Phantom
{
	public class Tester : MonoBehaviour
	{
		public GameObject prefab;
		public ShipGenerator shipGenerator;
		public ShipDesign shipDesign;
		public Color[] colors;
		public TilePathAgent pathAgent;
		public Path<Vector2Int> path;

		// Start is called before the first frame update
		void Start()
		{
			shipDesign = shipGenerator.Create(64, 64);
			Debug.Log(shipDesign.BoundingBox);

			var ship = shipDesign.Create(prefab);
			Debug.Log(ship.GetComponent<StatSheet>());

			path = pathAgent.FindPath(shipDesign.tiles, Vector2Int.zero, new Vector2Int(0, 16));
		}

		// Update is called once per frame
		void Update()
		{
		}

		public void OnChanged(Event e)
		{
			Debug.Log(e);
			//Debug.Log(e.Context);
		}

		private void OnDrawGizmos()
		{
			var _color = Gizmos.color;

			if (path == null || path.Cells == null) return;

			Gizmos.color = Color.cyan;
			foreach (var cell in path.Cells)
			{
				Gizmos.DrawWireSphere((Vector3Int)cell, 0.5f);
			}

			Gizmos.color = _color;
		}

		/*
				private void OnDrawGizmos()
				{
					if (tileMap.Width < 1)
						return;

					var groups = tileMap.vertices.FloodFindGroups((a, b) => a == b);
					var _color = Gizmos.color;

					if (colors.Length < groups.Count)
					{
						colors = new Color[groups.Count];
						for (int i = 0; i < colors.Length; i++)
							colors[i] = Random.ColorHSV();
					}

					for (int i = 0; i < colors.Length; i++)
					{
						Gizmos.color = colors[i];
						foreach (var cell in groups[i])
						{
							var pos = new Vector3(cell.x, cell.y, 0);
							Gizmos.DrawWireSphere(pos, 0.5f);
						}
					}

					Gizmos.color = _color;
				}
		*/
	}
}

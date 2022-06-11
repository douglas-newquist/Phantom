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
		public MapGenerator mapGenerator;
		public LevelDesign levelDesign;
		public ShipDesign shipDesign;
		public Color[] colors;
		public TilePathAgent pathAgent;
		public Path<Vector2Int> path;
		public TileMapTexture mapTexture;
		float nextPath = 0;

		public StatSO stat1, stat2;
		public float stat1Value;
		public ModifierSO modifierType;
		public float modifierMagnitude;

		GameObject ship;

		// Start is called before the first frame update
		void Start()
		{
			var stats = GetComponent<StatSheet>();

			stats.GetStat(stat1).BaseValue = stat1Value;
			stats.GetStat(stat2).AddModifier(modifierType.Create(modifierMagnitude));

			Debug.Log(stats);
			stats.GetStat(stat1).AddModifier(new MultiplierModifier(null, 0, true, 2));
			Debug.Log(stats);

			shipDesign = shipGenerator.Create(64, 64);
			Debug.Log(shipDesign.BoundingBox);

			ship = shipDesign.Create(prefab);
			Debug.Log(ship.GetComponent<StatSheet>());

			//	path = pathAgent.FindPath(shipDesign.tiles, Vector2Int.zero, new Vector2Int(63, 63));

			levelDesign = mapGenerator.Create(128, 128);
			levelDesign.tileMapTexture = mapTexture;
			levelDesign.Create();
		}

		// Update is called once per frame
		void Update()
		{
			if (Time.time >= nextPath)
			{
				nextPath = Time.time + 5;
				var pos = ship.transform.position;
				path = pathAgent.FindPath(levelDesign.tiles, Vector2Int.zero, new Vector2Int((int)pos.x, (int)pos.y));
			}
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Phantom.Pathfinding;
using UnityEngine.Tilemaps;

namespace Phantom
{
	public class Tester : MonoBehaviour
	{
		[SerializeField]
		public IUsable usable;
		public GameObject prefab;
		public ShipGenerator shipGenerator;
		public ShipBuilder shipBuilder;
		public LevelGenerator mapGenerator;
		public LevelBuilder levelBuilder;
		public Color[] colors;
		public TilePathAgent pathAgent;
		public Path<Vector2Int> path;
		public TileMapTexture mapTexture;
		float nextPath = 0;

		public NameGenerator nameGenerator;

		public Tilemap tilemap;

		public TileBase tile;

		public TileMapSO weights;

		public ShipDesign shipDesign;
		public LevelDesign levelDesign;

		GameObject ship;

		// Start is called before the first frame update
		void Start()
		{
			shipDesign = shipGenerator.Create(32, 32);
			levelDesign = mapGenerator.Create(128, 128);
		}

		// Update is called once per frame
		void Update()
		{
			return;
			if (Time.time >= nextPath)
			{
				nextPath = Time.time + 2 * (float)path.Duration.TotalSeconds;
				var start = new Vector2Int((int)transform.position.x, (int)transform.position.y);
				var end = new Vector2Int((int)ship.transform.position.x, (int)ship.transform.position.y);
				//path = pathAgent.FindPath(levelDesign.Tiles, start, end);
				//	Debug.Log(path);
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

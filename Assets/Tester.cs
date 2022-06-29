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

		public SimpleFollowCameraExtension followCameraExtension;


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

		public void OnChanged(Event e)
		{
			Debug.Log(e);
			//Debug.Log(e.Context);
		}
	}
}

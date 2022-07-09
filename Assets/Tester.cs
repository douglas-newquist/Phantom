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

		public Phantom.UI.HealthBars healthBars;


		// Start is called before the first frame update
		void Start()
		{
			levelDesign = mapGenerator.Create();
			GameManager.CurrentLevel = levelBuilder.Create(levelDesign).GetComponent<Level>();

			shipDesign = shipGenerator.Create(16, 16);
			var shipName = shipBuilder.CreateRegister(shipDesign);
			var ship = ObjectPool.Spawn(shipName, new LevelSpawner());
			healthBars.SetStatSheet(ship);

			followCameraExtension.SetTarget(ship);

			for (int i = 0; i < 10; i++)
			{
				//ship = ObjectPool.Spawn(shipName, new LevelSpawner());
			}
		}
	}
}

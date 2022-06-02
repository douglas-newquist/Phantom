using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
	public class Tester : MonoBehaviour
	{
		public TileMapTexture tileMapTexture;
		public TileMap tileMap;
		public GridGen<int> tileMapGenerator;
		// Start is called before the first frame update
		void Start()
		{
			tileMap = new TileMap(tileMapGenerator.Create(64, 64));

			Debug.Log(tileMap.BoundingBox);

			var sprite = tileMapTexture.DrawSprite(tileMap);
			var renderer = gameObject.GetComponent<SpriteRenderer>();
			renderer.sprite = sprite;
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
	}
}

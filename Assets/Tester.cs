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
		// Start is called before the first frame update
		void Start()
		{
			tileMap = new TileMap(32, 32);

			for (int x = 0; x < tileMap.vertices.Width; x++)
				for (int y = 0; y < tileMap.vertices.Height; y++)
					if (Random.Range(0f, 1f) < 0.5f)
						tileMap.vertices.Set(x, y, 1);

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

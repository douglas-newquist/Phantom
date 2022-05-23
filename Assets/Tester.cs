using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
	public class Tester : MonoBehaviour
	{
		public TileMap map;
		public TileMapTexture tileMapTexture;
		public Tile a, b, c;

		// Start is called before the first frame update
		void Start()
		{
			map = new TileMap(32, 32);

			for (int x = 0; x < map.VertexWidth; x++)
				for (int y = 0; y < map.VertexHeight; y++)
				{
					if (Random.Range(0f, 1f) < 0.5f)
						map.SetVertex(x, y, 1);
				}

			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
			renderer.sprite = tileMapTexture.DrawSprite(map);
		}

		// Update is called once per frame
		void Update()
		{
			b = a ^ Tile.Full;
		}

		public void OnChanged(Event e)
		{
			Debug.Log(e);
			//Debug.Log(e.Context);
		}
	}
}

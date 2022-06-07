using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Phantom
{
	public class Tester : MonoBehaviour
	{
		public GameObject prefab;
		public ShipGenerator shipGenerator;
		public ShipDesign shipDesign;
		public Color[] colors;

		// Start is called before the first frame update
		void Start()
		{
			shipDesign = shipGenerator.Create(64, 64);

			Debug.Log(shipDesign.BoundingBox);

			shipDesign.Create(prefab);
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

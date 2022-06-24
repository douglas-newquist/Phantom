using UnityEngine;
using UnityEngine.UI;

namespace Phantom
{
	public class MiniMap : MonoBehaviour, IGrid2D<Color>
	{
		public Image image;

		public Color unexploredColor = Color.black;

		public Texture2D map;

		public int Width => map.width;

		public int Height => map.height;

		public Vector2Int Size => new Vector2Int(Width, Height);

		public void Clear()
		{
			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					map.SetPixel(x, y, unexploredColor);
		}

		public IGrid2D<Color> Clone()
		{
			throw new System.NotImplementedException();
		}

		public Color Get(int x, int y)
		{
			return map.GetPixel(x, y);
		}

		public bool InBounds(int x, int y)
		{
			throw new System.NotImplementedException();
		}

		public void Set(int x, int y, Color value)
		{
			map.SetPixel(x, y, value);
		}

		public bool TryGet(int x, int y, out Color value)
		{
			if (InBounds(x, y))
			{
				value = Get(x, y);
				return true;
			}

			value = unexploredColor;
			return false;
		}

		public bool TrySet(int x, int y, Color value)
		{
			throw new System.NotImplementedException();
		}
	}
}

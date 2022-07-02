using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Decaying Circle")]
	public class RandomCircleDecayingGridGen : VertexGenerator
	{
		[MinMax(0, 1)]
		public FloatRange chance = new FloatRange(0.5f, 0.5f);

		public int value = 1;

		protected override VertexTileMap ApplyOnce(VertexTileMap design, RectInt area)
		{
			design = new VertexTileMap(design);
			var center = area.center;
			var maxDistance = area.xMax - center.x;
			var threshHold = chance.Random;

			for (int x = area.xMin; x <= area.xMax; x++)
			{
				for (int y = area.yMin; y <= area.yMax; y++)
				{
					var distance = Vector2.Distance(center, new Vector2(x, y));
					distance /= maxDistance;
					distance *= threshHold;
					if (Random.Range(0f, 1f) >= distance)
						design.Vertices.TrySet(x, y, value);
				}
			}

			return design;
		}
	}
}

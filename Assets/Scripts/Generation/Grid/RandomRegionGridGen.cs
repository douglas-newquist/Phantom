using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Random Sub Region")]
	public class RandomRegionGridGen : GridGen
	{
		[MinMax(0f, 1f)]
		public FloatRange width = 0.5f, height = 0.5f;

		public GridGen regionGenerator;

		public override VertexTileMap ApplyOnce(VertexTileMap design, RectInt area)
		{
			int regionWidth = (int)(area.width * width.Random);
			int regionHeight = (int)(area.height * height.Random);
			int x = Random.Range(area.xMin, area.xMax - regionWidth);
			int y = Random.Range(area.yMin, area.yMax - regionHeight);

			var region = new RectInt(x, y, regionWidth, regionHeight);
			return regionGenerator.Apply(design, region);
		}
	}
}

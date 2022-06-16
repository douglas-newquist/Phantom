using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Applies a tile generator
	/// </summary>
	[CreateAssetMenu(menuName = CreateMenu.TileObjectMapGenerator + "Apply Tile Generator")]
	public class ApplyTileGeneratorTileObjectMapGenerator : TileObjectMapGenerator
	{
		public override TileObjectMap ApplyOnce(TileObjectMap design, RectInt area)
		{
			design = (TileObjectMap)design.Clone();
			design.Tiles.Vertices = tileGenerator.Apply(design.Tiles.Vertices, area);
			return design;
		}
	}
}

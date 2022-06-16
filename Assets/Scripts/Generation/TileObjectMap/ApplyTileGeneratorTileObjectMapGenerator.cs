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
			design = new TileObjectMap(design);
			design.tiles.vertices = tileGenerator.Apply(design.tiles.vertices, area);
			return design;
		}
	}
}

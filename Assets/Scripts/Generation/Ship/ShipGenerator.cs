using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipGenerator + "Ship")]
	public class ShipGenerator : ScriptableObject
	{
		[Tooltip("Vertex generator to use if creating a new design")]
		public GridGen vertexGenerator;

		public TileObjectMapGenerator tileObjectMapGenerator;

		/// <summary>
		/// Creates a new ship design of the specified size
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public ShipDesign Create(int width, int height)
		{
			var design = new ShipDesign(width, height);

			if (vertexGenerator == null)
				throw new System.ArgumentNullException("Vertex generator not assigned on " + this.name + ".");

			if (tileObjectMapGenerator == null)
				throw new System.ArgumentNullException("Tile object map generator not assigned on " + this.name + ".");

			design.Tiles = vertexGenerator.Apply(design.Tiles);
			design = tileObjectMapGenerator.Apply(design) as ShipDesign;

			if (design == null)
				throw new System.RankException("Tile object map generator didn't return a ship design.");

			return design;
		}
	}
}

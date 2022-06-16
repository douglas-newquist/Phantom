using UnityEngine;

namespace Phantom
{
	public abstract class ShipGenerator : Generator<ShipDesign>
	{
		[Tooltip("Vertex generator to use if creating a new design")]
		public GridGen vertexGenerator;

		/// <summary>
		/// Applies this generator to the entire area of the ship design
		/// </summary>
		/// <param name="design"></param>
		/// <returns></returns>
		public override ShipDesign Apply(ShipDesign design)
		{
			var area = new RectInt(0, 0, design.Width, design.Height);
			return Apply(design, area);
		}

		/// <summary>
		/// Creates a new ship design of the specified size
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public override ShipDesign Create(int width, int height)
		{
			var design = new ShipDesign(width, height);
			if (vertexGenerator != null)
				design.Tiles.Vertices = vertexGenerator.Apply(design.Tiles.Vertices);
			return Apply(design);
		}
	}
}

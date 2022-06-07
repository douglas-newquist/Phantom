using UnityEngine;

namespace Phantom
{
	public abstract class ShipGenerator : ScriptableObject
	{
		[MinMax(0, 16)]
		public IntRange repeat = new IntRange(1, 1);

		[Tooltip("Vertex generator to use if creating a new design")]
		public GridGen vertexGenerator;

		public abstract ShipDesign ApplyOnce(ShipDesign design, RectInt area);

		/// <summary>
		/// Applies this generator to a specific area
		/// </summary>
		/// <param name="design"></param>
		/// <param name="area">Area to apply too</param>
		/// <returns></returns>
		public virtual ShipDesign Apply(ShipDesign design, RectInt area)
		{
			for (int repeats = repeat.Random; repeats > 0; repeats--)
				design = ApplyOnce(design, area);

			return design;
		}

		/// <summary>
		/// Applies this generator to the entire area of the ship design
		/// </summary>
		/// <param name="design"></param>
		/// <returns></returns>
		public virtual ShipDesign Apply(ShipDesign design)
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
		public virtual ShipDesign Create(int width, int height)
		{
			var design = new ShipDesign(width, height);
			if (vertexGenerator != null)
				design.tiles.vertices = vertexGenerator.Apply(design.tiles.vertices);
			return Apply(design);
		}
	}
}

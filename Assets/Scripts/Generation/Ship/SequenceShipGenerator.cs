using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Applies multiple ShipGenerators in a row
	/// </summary>
	[CreateAssetMenu(menuName = CreateMenu.ShipGenerator + "Sequence")]
	public class SequenceShipGenerator : ShipGenerator
	{
		public ShipGenerator[] generators;

		public override ShipDesign ApplyOnce(ShipDesign design, RectInt area)
		{
			foreach (var generator in generators)
				if (generator != null)
					design = generator.Apply(design, area);

			return design;
		}
	}
}

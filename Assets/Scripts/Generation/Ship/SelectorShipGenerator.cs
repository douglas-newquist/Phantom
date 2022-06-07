using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Chooses a random ShipGenerator to use and apply
	/// </summary>
	[CreateAssetMenu(menuName = CreateMenu.ShipGenerator + "Selector")]
	public class SelectorShipGenerator : ShipGenerator
	{
		public WeightedList<ShipGenerator> generators;

		public override ShipDesign ApplyOnce(ShipDesign design, RectInt area)
		{
			var generator = generators.GetRandom();

			if (generator != null)
				return generator.Apply(design, area);
			return design;
		}
	}
}

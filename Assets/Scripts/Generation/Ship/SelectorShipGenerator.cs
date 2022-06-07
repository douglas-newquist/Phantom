using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Generators/Ships/Selector")]
	public class SelectorShipGenerator : ShipGenerator
	{
		public WeightedList<ShipGenerator> generators;

		public override ShipDesign ApplyOnce(ShipDesign design, RectInt area)
		{
			var choice = generators.GetRandom();

			if (choice != null)
				return choice.Apply(design, area);
			return design;
		}
	}
}

using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Generation/Ships/Sequence")]
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

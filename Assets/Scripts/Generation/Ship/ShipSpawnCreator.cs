using UnityEngine;
using Phantom.ObjectPooling;

namespace Phantom
{
	/// <summary>
	/// Creates new ship objects from a design
	/// </summary>
	public class ShipSpawnCreator : ISpawnFactory
	{
		public ShipBuilder builder;
		public ShipDesign design;

		public ShipSpawnCreator(ShipBuilder builder, ShipDesign design)
		{
			this.builder = builder;
			this.design = design;
		}

		public GameObject Create()
		{
			return builder.Create(design);
		}
	}
}

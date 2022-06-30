using UnityEngine;
using Phantom.ObjectPooling;

namespace Phantom
{
	/// <summary>
	/// Creates new ship objects from a design
	/// </summary>
	public class ShipSpawnCreator : ISpawnFactory
	{
		[SerializeField]
		private ShipBuilder builder;

		public ShipBuilder Builder
		{
			get => builder;
			set
			{
				if (value == null)
					throw new System.ArgumentNullException("Builder");
				builder = value;
			}
		}

		[SerializeField]
		private ShipDesign design;

		public ShipDesign Design
		{
			get => design;
			set
			{
				if (value == null)
					throw new System.ArgumentNullException("Design");
				design = value;
			}
		}

		public ShipSpawnCreator(ShipBuilder builder, ShipDesign design)
		{
			Builder = builder;
			Design = design;
		}

		public GameObject Create()
		{
			return Builder.Create(Design);
		}
	}
}

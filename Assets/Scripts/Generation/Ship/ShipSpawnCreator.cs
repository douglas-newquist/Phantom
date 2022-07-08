using System.Collections.Generic;
using UnityEngine;
using Phantom.ObjectPooling;

namespace Phantom
{
	/// <summary>
	/// Creates new ship objects from a design
	/// </summary>
	public sealed class ShipSpawnCreator : ISpawnFactory
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

		private List<ISpawner> spawners = new List<ISpawner>();

		public ShipSpawnCreator(ShipBuilder builder, ShipDesign design, params ISpawner[] spawners)
		{
			Builder = builder;
			Design = design;

			if (spawners != null)
				this.spawners.AddRange(spawners);
		}

		public GameObject Create()
		{
			var ship = Builder.Create(Design);

			foreach (var spawner in spawners)
				if (spawner != null)
					spawner.Spawn(ship);

			return ship;
		}

		public void AddSpawner(ISpawner spawner)
		{
			spawners.Add(spawner);
		}

		public bool RemoveSpawner(ISpawner spawner)
		{
			return spawners.Remove(spawner);
		}
	}
}

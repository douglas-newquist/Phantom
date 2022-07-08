using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu + "Combat Sensor")]
	public sealed class CombatWorldSensor : WorldSensor
	{
		public override WorldState GetWorldState(GameObject gameObject)
		{
			if (gameObject.TryGetComponent<IEntity>(out var entity))
				return new WorldState(Key, entity.InCombat);
			return new WorldState(Key, false);
		}
	}
}

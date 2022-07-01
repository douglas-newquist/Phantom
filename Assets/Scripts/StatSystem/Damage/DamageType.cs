using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = DamageType.CreateMenu + "Damage Type")]
	public class DamageType : ScriptableObject
	{
		public const string CreateMenu = "Game/Stat System/";

		public DamageMultiplier[] multipliers;

		public override string ToString()
		{
			return name;
		}

		public float Scale(ResourceStatType type, float amount)
		{
			foreach (var multiplier in multipliers)
				if (multiplier.resource == type)
					return multiplier.Scale(amount);

			return amount;
		}

		public float Descale(ResourceStatType type, float amount)
		{
			foreach (var multiplier in multipliers)
				if (multiplier.resource == type)
					return multiplier.Descale(amount);

			return amount;
		}
	}
}

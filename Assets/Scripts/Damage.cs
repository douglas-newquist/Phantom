using UnityEngine;

namespace Game
{
	[System.Serializable]
	public struct Damage
	{
		public DamageType damageType;

		public float amount;

		public Damage(DamageType damageType, float amount)
		{
			this.damageType = damageType;
			this.amount = amount;
		}

		public Damage Apply(StatSheet sheet)
		{
			float remaining = amount;

			foreach (var multiplier in damageType.multipliers)
			{
				if (Mathf.Abs(remaining) < 0.1f)
					return new Damage(damageType, 0);

				var resource = sheet.GetStat<ResourceStat>(multiplier.resource);
				var taken = resource.Withdraw(remaining * multiplier.multiplier);
				remaining -= taken / multiplier.multiplier;
			}

			return new Damage(damageType, remaining);
		}
	}
}

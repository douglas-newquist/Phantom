using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class Damage
	{
		public object source;

		public DamageType damageType;

		public float amount;

		public Damage(object source, DamageType damageType, float amount)
		{
			this.source = source;
			this.damageType = damageType;
			this.amount = amount;
		}

		public void Apply(StatSheet sheet)
		{
			float remaining = amount;

			foreach (var multiplier in damageType.multipliers)
			{
				if (Mathf.Abs(remaining) < 0.1f)
					return;

				var resource = sheet.GetStat<ResourceStat>(multiplier.resource);
				var taken = resource.Withdraw(remaining * multiplier.multiplier);
				remaining -= taken / multiplier.multiplier;
			}
		}
	}
}

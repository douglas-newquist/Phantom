using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public struct Damage
	{
		public object source;

		public DamageType damageType;

		public float amount;

		public Damage(object source, DamageType damageType, float amount)
		{
			this.source = source;
			this.damageType = damageType;
			this.amount = amount;

			if (damageType == null)
				throw new System.ArgumentNullException("Damage type is null");
		}

		public Damage(Damage other)
		{
			source = other.source;
			damageType = other.damageType;
			amount = other.amount;
		}

		public override string ToString()
		{
			if (source != null)
				return string.Format("{0} {1} Damage from {2}", amount, damageType, source);
			return string.Format("{0} {1} Damage", amount, damageType);
		}

		public void Apply(StatSheet sheet)
		{
			float remaining = amount;

			if (damageType == null)
				throw new System.ArgumentNullException("Damage type is null");

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

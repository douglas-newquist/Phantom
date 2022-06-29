using UnityEngine;

namespace Phantom.StatSystem
{
	[System.Serializable]
	public struct Damage
	{
		private object source;

		public object Source { get => source; set => source = value; }

		[SerializeField]
		private DamageType damageType;

		public DamageType DamageType { get => damageType; set => damageType = value; }

		[SerializeField]
		private float amount;

		public float Amount { get => amount; set => amount = value; }

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
			this.source = other.Source;
			this.damageType = other.DamageType;
			this.amount = other.Amount;
		}

		public override string ToString()
		{
			if (Source != null)
				return string.Format("{0} {1} Damage from {2}", Amount, DamageType, Source);
			return string.Format("{0} {1} Damage", Amount, DamageType);
		}

		public void Apply(StatSheet sheet)
		{
			float remaining = Amount;

			if (DamageType == null)
				throw new System.ArgumentNullException("Damage type is null");

			foreach (var multiplier in DamageType.multipliers)
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

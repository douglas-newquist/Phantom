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
				var resource = sheet.GetStat<ResourceStat>(multiplier.resource);
				var taken = resource.Withdraw(amount * multiplier.multiplier);
				UnityEngine.Debug.Log("Taken " + taken);
				remaining -= taken / multiplier.multiplier;
			}

			return new Damage(damageType, remaining);
		}
	}
}

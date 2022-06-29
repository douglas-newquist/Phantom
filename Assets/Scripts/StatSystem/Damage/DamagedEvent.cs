namespace Phantom.StatSystem
{
	public class DamagedEvent : Event
	{
		/// <summary>
		/// How much damage was to be applied originally
		/// </summary>
		public Damage OriginalDamage { get; private set; }

		/// <summary>
		/// Current amount of damage to apply
		/// </summary>
		public Damage Damage { get; set; }

		public DamagedEvent(object context, Damage damage) : base(context)
		{
			OriginalDamage = damage;
			Damage = damage;
		}

		public override string ToString()
		{
			return string.Format("{0} to {1}", Damage, Context);
		}
	}
}

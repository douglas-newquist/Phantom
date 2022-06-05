namespace Game
{
	public class DamagedEvent : Event
	{
		public Damage OriginalDamage { get; private set; }

		public Damage Damage { get; protected set; }

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

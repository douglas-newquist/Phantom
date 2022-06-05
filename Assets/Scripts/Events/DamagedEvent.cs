namespace Game
{
	public class DamagedEvent : Event
	{
		public Damage Damage { get; protected set; }

		public DamagedEvent(object context, Damage damage) : base(context)
		{
			Damage = damage;
		}

		public override string ToString()
		{
			return string.Format("{0} to {1}", Damage, Context);
		}
	}
}

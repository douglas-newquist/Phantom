namespace Game
{
	public class ProjectileFiredEvent : Event
	{
		public Projectile Projectile { get; private set; }

		public ProjectileFiredEvent(object context, Projectile projectile) : base(context)
		{
			Projectile = projectile;
		}

		public override string ToString()
		{
			return "Fired projectile " + Projectile;
		}
	}
}

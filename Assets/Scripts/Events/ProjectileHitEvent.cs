namespace Phantom
{
	public class ProjectileHitEvent : Event
	{
		public Projectile Projectile { get; private set; }

		public ProjectileHitEvent(object context, Projectile projectile) : base(context)
		{
			Projectile = projectile;
		}

		public override string ToString()
		{
			return "Projectile " + Projectile + " hit " + Context;
		}
	}
}

using UnityEngine;

namespace Phantom
{
	public class ProjectileFiredEvent : Event
	{
		public GameObject Projectile { get; private set; }

		public ProjectileFiredEvent(object context, GameObject projectile) : base(context)
		{
			Projectile = projectile;
		}

		public override string ToString()
		{
			return "Fired projectile " + Projectile;
		}
	}
}

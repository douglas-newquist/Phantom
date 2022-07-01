using Phantom.StatSystem;
using UnityEngine;

namespace Phantom
{
	public interface IProjectile
	{
		float Acceleration { get; set; }

		float DeathTime { get; set; }

		Damage Damage { get; set; }

		void Fire(GameObject owner, float lifeSpan);
	}
}

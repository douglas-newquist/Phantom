using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
	public class BulletProjectile : Projectile
	{
		public Rigidbody2D body => GetComponent<Rigidbody2D>();

		public BulletProjectileSO BulletStats => (BulletProjectileSO)ProjectileStats;

		/// <summary>
		/// Sent when another object enters a trigger collider attached to this
		/// object (2D physics only).
		/// </summary>
		/// <param name="other">The other Collider2D involved in this collision.</param>
		void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log(other.name);
		}
	}
}
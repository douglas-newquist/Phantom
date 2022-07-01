using UnityEngine;
using Phantom.StatSystem;
using Phantom.ObjectPooling;

namespace Phantom
{
	[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
	public class KinematicProjectile : Projectile
	{
		protected Rigidbody2D body;

		protected virtual void Start()
		{
			body = GetComponent<Rigidbody2D>();
		}

		protected virtual void FixedUpdate()
		{
			Vector3 velocity = body.velocity;
			velocity += transform.up * Time.fixedDeltaTime * Acceleration;

			if (velocity.magnitude > GameManager.SpeedLimit)
				velocity = velocity.normalized * GameManager.SpeedLimit;

			body.velocity = velocity;
		}

		/// <summary>
		/// Sent when another object enters a trigger collider attached to this
		/// object (2D physics only).
		/// </summary>
		/// <param name="other">The other Collider2D involved in this collision.</param>
		void OnTriggerEnter2D(Collider2D other)
		{
			var damageable = other.GetComponent<IDamageable>();
			if (damageable == null) return;

			damageable.ApplyDamage(Damage);
			OnExpired.Invoke(gameObject);
			OnHit.Invoke(other.gameObject);
			ObjectPool.Despawn(gameObject);
		}
	}
}

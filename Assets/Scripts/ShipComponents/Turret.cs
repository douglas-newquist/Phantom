using UnityEngine;

namespace Game
{
	public abstract class Turret : MonoBehaviour, ITurret
	{
		[Range(0f, 180f)]
		public float angleTolerance = 15;

		public virtual float ProjectileVelocity => throw new System.NotImplementedException();

		public virtual Vector3 PredictTargetPosition(Rigidbody2D body)
		{
			return body.position + body.velocity;
		}

		public virtual bool Fire(Vector3 vector, Reference mode)
		{
			var angle = Look(vector, mode);
			throw new System.NotImplementedException();
		}

		public abstract float Look(Vector3 vector, Reference mode);

		/// <summary>
		/// Fires this turret at a moving target
		/// </summary>
		/// <param name="turret"></param>
		/// <param name="body">What moving target to fire at</param>
		/// <returns>True if the turret actually fired a projectile</returns>
		public virtual bool FireAt(Rigidbody2D body)
		{
			return Fire(PredictTargetPosition(body), Reference.Absolute);
		}

		/// <summary>
		/// Points this turret at a moving target
		/// </summary>
		/// <param name="body">Target to track</param>
		/// <returns>The angle difference between this turret and the target</returns>
		public float LookAt(Rigidbody2D body)
		{
			return Look(PredictTargetPosition(body), Reference.Absolute);
		}

		public virtual void Reset() { }
	}
}

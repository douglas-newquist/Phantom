using UnityEngine;
using UnityEngine.Events;
using Phantom.StatSystem;
using Phantom.ObjectPooling;

namespace Phantom
{
	[DisallowMultipleComponent]
	public class Entity : MonoBehaviour, IEntity
	{
		public StatSheet Stats => GetComponent<StatSheet>();

		public bool InCombat => Time.time > lastCombat + 60;

		public bool IsAlive => throw new System.NotImplementedException();

		private float lastCombat = float.NegativeInfinity;

		public virtual void OnTakeDamage(DamagedEvent damagedEvent)
		{
			lastCombat = Time.time;
		}

		public virtual void OnDeath()
		{
			ObjectPool.Despawn(gameObject);
		}

		public Attitude GetAttitudeTowards(IEntity other)
		{
			throw new System.NotImplementedException();
		}
	}
}

using UnityEngine;
using UnityEngine.Events;
using Phantom.StatSystem;
using Phantom.ObjectPooling;

namespace Phantom
{
	[DisallowMultipleComponent]
	public class Entity : MonoBehaviour
	{
		public StatSheet Stats => GetComponent<StatSheet>();

		public void OnTakeDamage(DamagedEvent e)
		{
			Debug.Log(e);
		}

		public void OnTakeFatalDamage(DamagedEvent e)
		{
			Debug.Log(e);
		}

		public virtual void OnDeath()
		{
			ObjectPool.Despawn(gameObject);
			Debug.Log("Death");
		}
	}
}

using UnityEngine;
using UnityEngine.Events;
using Phantom.StatSystem;

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
			Destroy(gameObject);
			Debug.Log("Death");
		}
	}
}

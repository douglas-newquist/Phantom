using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	[DisallowMultipleComponent]
	public class Entity : MonoBehaviour
	{
		public StatSheet Stats => GetComponent<StatSheet>();

		public Damage damage;

		private void Start()
		{
			Stats.GetStat(Stats.primaryHealthStat).AddModifier(new AdditiveModifier(null, 0, false, 1000));
		}

		private void Update()
		{
			Stats.ApplyDamage(damage);
		}

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

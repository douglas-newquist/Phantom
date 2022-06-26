using UnityEngine;
using UnityEngine.Events;

namespace Phantom
{
	[DisallowMultipleComponent]
	public class Entity : MonoBehaviour
	{
		public StatSheet Stats => GetComponent<StatSheet>();

		public Damage damage;

		private void Start()
		{
			Stats.GetStat(Stats.PrimaryHealthStat).AddModifier(new AdditiveModifier(null, 0, false, 1000));
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

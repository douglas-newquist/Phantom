using Phantom.ObjectPooling;
using Phantom.StatSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Phantom
{
	[DisallowMultipleComponent]
	public abstract class Entity : MonoBehaviour, IEntity
	{
		public StatSheet Stats => GetComponent<StatSheet>();

		[SerializeField]
		[Range(-1f, 60 * 60)]
		private float combatTimeOutAfter = 60;

		/// <summary>
		/// How many seconds after getting attacked should this entity calm down
		/// </summary>
		public float CombatTimeOutAfter
		{
			get => combatTimeOutAfter;
			set => combatTimeOutAfter = value;
		}

		private float lastCombatTime = float.NegativeInfinity;

		/// <summary>
		/// The last time that this entity was attacked or entered combat
		/// </summary>
		public float LastCombatTime
		{
			get => lastCombatTime;
			set => lastCombatTime = Mathf.Clamp(value, 0, Time.time);
		}

		/// <summary>
		/// Gets the time this entity will automatically exit combat
		/// </summary>
		public float CombatEndTime
		{
			get
			{
				if (CombatTimeOutAfter <= 0)
					return float.MaxValue;
				return LastCombatTime + CombatTimeOutAfter;
			}
		}

		public bool InCombat { get; protected set; }

		[SerializeField]
		private UnityEvent<IEntity> onEnterCombat, onExitCombat;

		public UnityEvent<IEntity> OnEnterCombat => onEnterCombat;

		public UnityEvent<IEntity> OnExitCombat => onExitCombat;

		public virtual bool IsAlive
		{
			get
			{
				if (TryGetComponent(out HealthTracker health))
					return health.IsAlive;
				return true;
			}
		}

		private HashSet<IEntity> attackers = new HashSet<IEntity>();

		public IEnumerable<IEntity> AttackedBy => attackers;

		protected virtual void Update()
		{
			if (InCombat && Time.time >= CombatEndTime)
			{
				InCombat = false;
				OnExitCombat.Invoke(this);
			}
		}

		public virtual void OnTakeDamage(DamagedEvent damagedEvent)
		{
			if (!InCombat)
			{
				InCombat = true;
				OnEnterCombat.Invoke(this);
			}

			LastCombatTime = Time.time;
			if (damagedEvent.Damage.Source == null)
				return;
			if (damagedEvent.Damage.Source is IEntity)
				attackers.Add(damagedEvent.Damage.Source as IEntity);
		}

		public void EnterCombat(IEntity entity)
		{
			Debug.Log("Entered combat");
		}

		public void ExitCombat(IEntity entity)
		{
			Debug.Log("Exited combat");
		}

		public virtual void OnDeath()
		{
			ObjectPool.Despawn(gameObject);
		}

		public Attitude GetAttitudeTowards(IEntity other)
		{
			if (other == null) return Attitude.NoAttitude;
			throw new System.NotImplementedException();
		}
	}
}

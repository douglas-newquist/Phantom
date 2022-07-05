using UnityEngine;
using UnityEngine.Events;

namespace Phantom.StatSystem
{
	[System.Serializable]
	public class ResourceStat : Stat, IResourceStat
	{
		public ResourceStatType ResourceType => (ResourceStatType)Type;

		public enum Changed
		{
			KeepCurrent,
			KeepDeltaMax,
			KeepPercentage
		}

		[SerializeField]
		protected Changed maxChangedMode = Changed.KeepPercentage;

		public Changed MaxChangedMode
		{
			get => maxChangedMode;
			set => maxChangedMode = value;
		}

		[SerializeField]
		protected float current;

		public float Current
		{
			get
			{
				if (Dirty)
					Recalculate();

				return current;
			}
			set
			{
				float old = current;
				current = Mathf.Clamp(value, 0, Value);

				if (old != current)
					OnCurrentChanged.Invoke(current);
			}
		}

		public bool Empty => Current <= 0;

		public bool Full => Current == Value;

		[SerializeField]
		protected UnityEvent<float> onCurrentChanged = new UnityEvent<float>();

		public UnityEvent<float> OnCurrentChanged => onCurrentChanged;

		public float Percentage
		{
			get => Math.ToPercentage(Current, 0, Value);
			set => Current = Math.FromPercentage(value, 0, Value);
		}

		public ResourceStat() : base()
		{ }

		public ResourceStat(ResourceStatType type, float maximum, float startPercent = 1) : base(type, maximum)
		{
			Percentage = startPercent;
		}

		public override string ToString()
		{
			return Type.name + " " + Current + " / " + Value + " (" + BaseValue + ")";
		}

		void OnMaxChanged(float old, float delta)
		{
			switch (maxChangedMode)
			{
				case Changed.KeepDeltaMax:
					Current += delta;
					break;

				case Changed.KeepPercentage:
					Percentage = Math.ToPercentage(Current, 0, old);
					break;

				default:
					Current = Current;
					break;
			}
		}

		public override void Recalculate()
		{
			float old = value;
			base.Recalculate();
			OnMaxChanged(old, Value - old);
		}

		/// <summary>
		/// Adds an amount of resource to the current supply
		/// </summary>
		/// <param name="amount">The amount of the resource to deposit</param>
		/// <param name="allOrNothing">If true, only deposits if the full amount can be deposited</param>
		/// <returns>The amount actually deposited</returns>
		public float Deposit(float amount, bool allOrNothing = false)
		{
			if (amount < 0)
				return -Withdraw(-amount, allOrNothing);

			if (allOrNothing && Current + amount > Value)
				return 0;

			if (Current + amount > Value)
				amount = Value - Current;

			Current += amount;
			return amount;
		}

		/// <summary>
		/// Withdraws an amount of this resource
		/// </summary>
		/// <param name="amount">Amount to withdraw</param>
		/// <param name="allOrNothing">If true, only withdraws if the full amount can be withdrawn</param>
		/// <returns>The amount actually withdrawn</returns>
		public float Withdraw(float amount, bool allOrNothing = false)
		{
			if (amount < 0)
				return -Deposit(-amount, allOrNothing);

			if (allOrNothing && Current < amount)
				return 0;

			if (amount > Current)
				amount = Current;

			Current -= amount;
			return amount;
		}

		/// <summary>
		/// Resets the current value it its starting value
		/// </summary>
		public void Reset()
		{
			Percentage = ResourceType.StartingPercentage;
		}
	}
}

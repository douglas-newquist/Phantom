using UnityEngine;
using UnityEngine.Events;

namespace Game
{

	[System.Serializable]
	public class ResourceStat : Stat
	{
		public ResourceStatSO ResourceType => (ResourceStatSO)Type;

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
					OnCurrentChanged.Invoke(new ValueChangedEvent(this, old, current));
			}
		}

		[SerializeField]
		protected UnityEvent<ValueChangedEvent> onCurrentChanged = new UnityEvent<ValueChangedEvent>();

		public UnityEvent<ValueChangedEvent> OnCurrentChanged => onCurrentChanged;

		public float Percentage
		{
			get => Math.ToPercentage(Current, 0, Value);
			set => Current = Math.FromPercentage(value, 0, Value);
		}

		public ResourceStat() : base()
		{ }

		public ResourceStat(ResourceStatSO type, float maximum, float startPercent = 1) : base(type, maximum)
		{
			Percentage = startPercent;
		}

		void OnMaxChanged(ValueChangedEvent change)
		{
			switch (maxChangedMode)
			{
				case Changed.KeepDeltaMax:
					Current += change.Delta;
					break;

				case Changed.KeepPercentage:
					Percentage = Math.ToPercentage(Current, 0, change.Old);
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
			OnMaxChanged(new ValueChangedEvent(this, old, Value));
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

			float expected = Current + amount;

			if (allOrNothing && expected > Value)
				return 0;

			Current += amount;
			return expected - Current;
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

			float expected = Current - amount;

			if (allOrNothing && expected < Value)
				return 0;

			Current -= amount;
			return Current - expected;
		}

		/// <summary>
		/// Resets the current value it its starting value
		/// </summary>
		public void Reset()
		{
			Percentage = ResourceType.startingPercentage;
		}
	}
}

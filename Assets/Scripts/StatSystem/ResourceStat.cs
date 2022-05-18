using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	public enum ResourceMaxChangedMode
	{
		KeepCurrent,
		KeepDeltaMax,
		KeepDeltaMin,
		KeepPercentage
	}

	[System.Serializable]
	public class ResourceStat : IResourceStat
	{
		[SerializeField]
		protected ResourceMaxChangedMode maxChangedMode = ResourceMaxChangedMode.KeepCurrent;

		[SerializeField]
		protected IStat minimum = new Stat(), maximum = new Stat();

		public IStat Maximum => maximum;

		public IStat Minimum => minimum;

		[SerializeField]
		protected float current;

		public float Current
		{
			get => current;
			set
			{
				float old = current;
				current = Mathf.Clamp(value, Minimum.Value, Maximum.Value);
				onCurrentChanged.Invoke(new ValueChangedEvent(this, old, current));
			}
		}

		public float Percentage
		{
			get => Math.ToPercentage(Current, Minimum.Value, Maximum.Value);
			set => Current = Math.FromPercentage(value, Minimum.Value, Maximum.Value);
		}

		public UnityEvent<ValueChangedEvent> onCurrentChanged;

		public ResourceStat()
		{
			minimum.OnValueChanged.AddListener(OnMinChanged);
			maximum.OnValueChanged.AddListener(OnMaxChanged);
		}

		void OnMaxChanged(ValueChangedEvent change)
		{
			switch (maxChangedMode)
			{
				case ResourceMaxChangedMode.KeepDeltaMax:
					Current += change.Delta;
					break;

				case ResourceMaxChangedMode.KeepPercentage:
					Percentage = Math.ToPercentage(Current, Minimum.Value, change.Old);
					break;

				default:
					Current = Current;
					break;
			}
		}

		void OnMinChanged(ValueChangedEvent change)
		{
			switch (maxChangedMode)
			{
				case ResourceMaxChangedMode.KeepDeltaMin:
					Current += change.Delta;
					break;

				case ResourceMaxChangedMode.KeepPercentage:
					Percentage = Math.ToPercentage(Current, change.Old, Maximum.Value);
					break;

				default:
					Current = Current;
					break;
			}
		}

		/// <summary>
		/// Adds an amount of resource to the current supply
		/// </summary>
		/// <param name="amount">The amount of the resource to deposit</param>
		/// <param name="allOrNothing">If true only deposits if the full amount can be deposited</param>
		/// <returns>The amount actually deposited</returns>
		public float Deposit(float amount, bool allOrNothing = false)
		{
			if (amount < 0)
				return Withdraw(-amount, allOrNothing);

			float expected = Current + amount;

			if (allOrNothing && expected > Maximum.Value)
				return 0;

			Current += amount;
			return expected - Current;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="allOrNothing">If true only withdraws if the full amount can be withdrawn</param>
		/// <returns>The amount actually withdrawn</returns>
		public float Withdraw(float amount, bool allOrNothing = false)
		{
			if (amount < 0)
				return Deposit(-amount, allOrNothing);

			float expected = Current - amount;

			if (allOrNothing && expected < Minimum.Value)
				return 0;

			Current -= amount;
			return Current - expected;
		}
	}
}

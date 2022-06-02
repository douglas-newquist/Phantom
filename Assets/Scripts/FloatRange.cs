using UnityEngine;

namespace Game
{
	[System.Serializable]
	public struct FloatRange : IRange<float>
	{
		[SerializeField]
		private float min, max;

		public float Min
		{
			get => min;
			set
			{
				min = value;

				if (min > max)
					Utilities.Swap(ref min, ref max);
			}
		}

		public float Max
		{
			get => max;
			set
			{
				max = value;

				if (max < min)
					Utilities.Swap(ref min, ref max);
			}
		}

		public float Random => UnityEngine.Random.Range(Min, Max);

		/// <summary>
		/// The difference between min and max
		/// </summary>
		public float Delta
		{
			get => max - min;
			set
			{
				float center = Center;
				min = center - Mathf.Abs(value) / 2;
				max = center + Mathf.Abs(value) / 2;
			}
		}

		/// <summary>
		/// The center point of this range
		/// </summary>
		public float Center
		{
			get => (min + max) / 2;
			set
			{
				float delta = Delta / 2;
				min = value - delta;
				max = value + delta;
			}
		}

		public FloatRange(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		/// <summary>
		/// Clamps a value inside this range
		/// </summary>
		public float Clamp(float value) => Mathf.Clamp(value, Min, Max);

		public float ToPercentage(float value) => Math.ToPercentage(value, Min, Max);

		public float FromPercentage(float value) => Math.FromPercentage(value, Min, Max);

		public static implicit operator FloatRange(float f)
		{
			return new FloatRange(f, f);
		}
	}
}

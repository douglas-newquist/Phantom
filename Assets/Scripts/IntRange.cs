using UnityEngine;

namespace Game
{
	[System.Serializable]
	public struct IntRange : IRange<int>
	{
		[SerializeField]
		private int min, max;

		public int Min
		{
			get => min;
			set
			{
				min = value;

				if (min > max)
					Utilities.Swap(ref min, ref max);
			}
		}

		public int Max
		{
			get => max;
			set
			{
				max = value;

				if (max < min)
					Utilities.Swap(ref min, ref max);
			}
		}

		public int Random => UnityEngine.Random.Range(Min, Max + 1);

		/// <summary>
		/// The difference between min and max
		/// </summary>
		public int Delta
		{
			get => max - min;
			set
			{
				int center = Center;
				min = center - Mathf.Abs(value) / 2;
				max = center + Mathf.Abs(value) / 2;
			}
		}

		/// <summary>
		/// The center point of this range
		/// </summary>
		public int Center
		{
			get => (min + max) / 2;
			set
			{
				int delta = Delta / 2;
				min = value - delta;
				max = value + delta;
			}
		}

		public IntRange(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		/// <summary>
		/// Clamps a value inside this range
		/// </summary>
		public int Clamp(int value) => Mathf.Clamp(value, Min, Max);

		public float ToPercentage(int value) => Math.ToPercentage(value, Min, Max);

		public int FromPercentage(float value) => (int)Math.FromPercentage(value, Min, Max);
	}
}

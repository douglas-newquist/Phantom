using UnityEngine;

namespace Game
{
	public static class Math
	{
		/// <summary>
		/// Converts a number inside a range to a percentage
		/// </summary>
		/// <param name="value">Current value</param>
		/// <param name="min">Lower bound</param>
		/// <param name="max">Upper bound</param>
		public static float ToPercentage(float value, float min, float max)
		{
			float divisor = max - min;
			value -= min;

			if (divisor == 0)
				return value == 0 ? 0 : 1;

			return value / divisor;
		}

		/// <summary>
		/// Converts a percentage to a number inside a range
		/// </summary>
		/// <param name="value">Current percentage</param>
		/// <param name="min">Lower bound</param>
		/// <param name="max">Upper bound</param>
		public static float FromPercentage(float value, float min, float max)
		{
			return value * (max - min) + min;
		}

		public static Vector2 RotateVector2(Vector2 vector, float radians)
		{
			return new Vector2(
				vector.x * Mathf.Cos(radians) - vector.y * Mathf.Sin(radians),
				vector.x * Mathf.Sin(radians) + vector.y * Mathf.Cos(radians)
			);
		}

		public static Vector2 Rotate(this Vector2 vector, float radians)
		{
			return RotateVector2(vector, radians);
		}
	}
}

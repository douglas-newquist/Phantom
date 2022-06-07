using UnityEngine;

namespace Phantom
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

		/// <summary>
		/// Calculates the location of a moving object after a given amount of time
		/// </summary>
		/// <param name="time"></param>
		/// <param name="start"></param>
		/// <param name="velocity"></param>
		/// <param name="acceleration"></param>
		public static Vector2 ProjectilePosition(float time, Vector2 start, Vector2 velocity, Vector2 acceleration)
		{
			return start + velocity * time + acceleration * time * time * 0.5f;
		}

		public static float ProjectileTravelTime(Vector2 start, Vector2 end, float velocity, float acceleration)
		{
			if (velocity == 0 && acceleration == 0)
				return float.PositiveInfinity;

			var distance = Vector2.Distance(start, end);

			if (acceleration == 0)
				return distance / velocity;

			var t = (Mathf.Sqrt(2f * acceleration * acceleration * distance + velocity * velocity) - velocity) / (acceleration * acceleration);

			return Mathf.Max(t, -t);
		}

		/// <summary>
		/// Predicts the location where two moving objects will collide
		/// </summary>
		/// <param name="start"></param>
		/// <param name="target"></param>
		/// <param name="v1">Velocity of the projectile</param>
		/// <param name="v2"></param>
		/// <param name="a1">Acceleration of the projectile</param>
		/// <param name="a2"></param>
		/// <returns></returns>
		public static Vector2 PredictImpact(Vector2 start, Vector2 target, float v1, Vector2 v2, float a1, Vector2 a2)
		{
			if (v2.magnitude < 0.1f && a2.magnitude < 0.1f)
				return target;

			var lastPrediction = target;
			var prediction = target;

			for (int i = 0; i < 10; i++)
			{
				var time = ProjectileTravelTime(start, prediction, v1, a1);
				lastPrediction = prediction;
				prediction = ProjectilePosition(time, target, v2, a2);
				if (Vector2.Distance(prediction, lastPrediction) < 1f)
					return prediction;
			}

			return prediction;
		}
	}
}

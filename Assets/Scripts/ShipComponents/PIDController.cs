using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class VectorPIDController : IPIDController<Vector3>
	{
		[SerializeField]
		private float proportionalGain = 1, integralGain = 1, derivativeGain = 1;

		[SerializeField]
		private float integralSaturation = 1;

		/// <summary>
		/// Scales corrective force linearly
		/// </summary>
		public float ProportionalGain
		{
			get => proportionalGain;
			set => proportionalGain = Mathf.Clamp(value, 0, float.MaxValue);
		}

		public float IntegralGain
		{
			get => integralGain;
			set => integralGain = Mathf.Clamp(value, 0, float.MaxValue);
		}

		/// <summary>
		/// Derivative Gain
		/// </summary>
		public float DerivativeGain
		{
			get => derivativeGain;
			set => derivativeGain = Mathf.Clamp(value, 0, float.MaxValue);
		}
		public float IntegralSaturation
		{
			get => integralSaturation;
			set => integralSaturation = value;
		}

		[SerializeField]
		private bool useVelocity = false;

		private Vector3 lastError;
		private Vector3 lastValue;
		private bool lastErrorValid = false;
		private Vector3 integral;

		public VectorPIDController(float p, float i, float d)
		{
			this.ProportionalGain = p;
			this.IntegralGain = i;
			this.DerivativeGain = d;
		}

		public VectorPIDController() { }

		public Vector3 Correction(Vector3 current, Vector3 target, float deltaTime)
		{
			Vector3 error = target - current;
			Vector3 P = Vector3.zero, I = Vector3.zero, D = Vector3.zero;

			P = error * ProportionalGain;

			if (lastErrorValid)
			{
				Vector3 errorRateOfChange = (error - lastError) / deltaTime;
				lastError = error;

				Vector3 valueRateOfChance = (current - lastValue) / deltaTime;
				lastValue = current;

				if (useVelocity)
					D = DerivativeGain * valueRateOfChance;
				else
					D = DerivativeGain * errorRateOfChange;
			}

			lastErrorValid = true;

			integral = integral + (error * deltaTime);
			integral.x = Mathf.Clamp(integral.x, -IntegralSaturation, IntegralSaturation);
			integral.y = Mathf.Clamp(integral.y, -IntegralSaturation, IntegralSaturation);
			integral.z = Mathf.Clamp(integral.z, -IntegralSaturation, IntegralSaturation);
			I = IntegralGain * integral;

			return P + I + D;
		}

		public void Reset()
		{
			lastErrorValid = false;
		}
	}
}

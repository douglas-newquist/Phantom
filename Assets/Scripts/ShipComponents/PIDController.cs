using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class VectorPIDController : IPIDController<Vector3>, IPIDController<Vector2>
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
			set => proportionalGain = value;
		}

		public float IntegralGain
		{
			get => integralGain;
			set => integralGain = value;
		}

		/// <summary>
		/// Derivative Gain
		/// </summary>
		public float DerivativeGain
		{
			get => derivativeGain;
			set => derivativeGain = value;
		}

		private Vector3 lastError;
		private bool lastErrorValid = false;
		private Vector3 integral;

		public VectorPIDController(float p, float i, float d)
		{
			this.ProportionalGain = p;
			this.IntegralGain = i;
			this.DerivativeGain = d;
		}

		public VectorPIDController()
		{
		}

		public Vector3 Correction(Vector3 error, float deltaTime)
		{
			Vector3 P = Vector3.zero, I = Vector3.zero, D = Vector3.zero;

			P = error * ProportionalGain;

			if (lastErrorValid)
			{
				Vector3 errorRateOfChange = (error - lastError) / deltaTime;
				lastError = error;

				D = DerivativeGain * errorRateOfChange;
			}

			lastErrorValid = true;

			integral = integral + (error * deltaTime);
			integral.x = Mathf.Clamp(integral.x, -integralSaturation, integralSaturation);
			integral.y = Mathf.Clamp(integral.y, -integralSaturation, integralSaturation);
			integral.z = Mathf.Clamp(integral.z, -integralSaturation, integralSaturation);
			I = IntegralGain * integral;

			return P + I + D;
		}

		public Vector2 Correction(Vector2 error, float deltaTime)
		{
			return Correction((Vector3)error, deltaTime);
		}

		public void Reset()
		{
			lastErrorValid = false;
		}
	}
}

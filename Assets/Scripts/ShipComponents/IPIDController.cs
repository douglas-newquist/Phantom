namespace Phantom
{
	public interface IPIDController<T>
	{
		/// <summary>
		/// Scales corrective force linearly
		/// </summary>
		float ProportionalGain { get; set; }

		float IntegralGain { get; set; }

		float DerivativeGain { get; set; }

		T Correction(T error, float deltaTime);

		void Reset();
	}
}

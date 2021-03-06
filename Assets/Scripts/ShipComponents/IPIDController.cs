namespace Phantom
{
	public interface IPIDController<T> : IReset
	{
		/// <summary>
		/// Scales corrective force linearly
		/// </summary>
		float ProportionalGain { get; set; }

		float IntegralGain { get; set; }

		float DerivativeGain { get; set; }

		T Correction(T current, T target, float deltaTime);
	}
}

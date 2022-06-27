namespace Phantom
{
	public interface IModifiableStat : IStat, IModifiable
	{
		/// <summary>
		/// Is the current value out of date
		/// </summary>
		bool Dirty { get; }

		/// <summary>
		/// Marks the current value as out of date
		/// </summary>
		void MarkDirty();

		/// <summary>
		/// Recalculates the current value
		/// </summary>
		void Recalculate();
	}
}

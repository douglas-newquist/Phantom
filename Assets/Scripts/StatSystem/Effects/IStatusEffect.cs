using UnityEngine;

namespace Phantom.StatSystem
{
	public interface IStatusEffect
	{
		StatusEffectType Type { get; }

		object Source { get; }

		bool IsRunning { get; }

		/// <summary>
		/// Nicely asks this status effect to cancel
		/// </summary>
		/// <returns>True if canceled</returns>
		bool TryCancel(StatSheet statSheet);

		/// <summary>
		/// Cancels this status effect
		/// </summary>
		void Cancel(StatSheet statSheet);

		void Apply(StatSheet statSheet);
	}
}

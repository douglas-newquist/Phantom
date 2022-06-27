using UnityEngine.Events;

namespace Phantom.StatSystem
{
	public interface IResourceStat : IModifiableStat
	{
		ResourceStatType ResourceType { get; }
		ResourceStat.Changed MaxChangedMode { get; set; }
		float Current { get; set; }
		float Percentage { get; set; }
		bool Empty { get; }
		bool Full { get; }
		UnityEvent<ValueChangedEvent> OnCurrentChanged { get; }

		void Reset();
		float Deposit(float amount, bool allOrNothing = false);
		float Withdraw(float amount, bool allOrNothing = false);
	}
}

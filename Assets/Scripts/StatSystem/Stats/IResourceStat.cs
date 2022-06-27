using UnityEngine.Events;

namespace Phantom.StatSystem
{
	public interface IResourceStat : IModifiableStat, IReset
	{
		ResourceStatType ResourceType { get; }
		ResourceStat.Changed MaxChangedMode { get; set; }
		float Current { get; set; }
		float Percentage { get; set; }
		bool Empty { get; }
		bool Full { get; }
		UnityEvent<ValueChangedEvent> OnCurrentChanged { get; }

		float Deposit(float amount, bool allOrNothing = false);
		float Withdraw(float amount, bool allOrNothing = false);
	}
}

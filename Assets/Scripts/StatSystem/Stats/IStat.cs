using UnityEngine.Events;

namespace Phantom
{
	public interface IStat
	{
		StatSheet Sheet { get; set; }
		StatSO Type { get; }
		float BaseValue { get; set; }
		UnityEvent<ValueChangedEvent> OnBaseValueChanged { get; }
		float Value { get; }
		UnityEvent<ValueChangedEvent> OnValueChanged { get; }
	}
}

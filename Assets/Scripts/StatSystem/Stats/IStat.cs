using UnityEngine.Events;

namespace Phantom
{
	public interface IStat
	{
		StatSheet Sheet { get; set; }
		StatType Type { get; }
		float BaseValue { get; set; }
		UnityEvent<ValueChangedEvent> OnBaseValueChanged { get; }
		float Value { get; }
		UnityEvent<ValueChangedEvent> OnValueChanged { get; }
	}
}

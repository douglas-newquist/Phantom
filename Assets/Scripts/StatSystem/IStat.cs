using UnityEngine.Events;

public interface IStat
{
	/// <summary>
	/// Gets/Sets the base value of this stat
	/// </summary>
	float BaseValue { get; set; }

	UnityEvent<ValueChangedEvent> OnBaseValueChanged { get; set; }

	/// <summary>
	/// Gets/Sets the current value of this stat after any modifiers
	/// </summary>
	float Value { get; set; }

	UnityEvent<ValueChangedEvent> OnValueChanged { get; set; }
}

using UnityEngine.Events;

namespace Phantom.StatSystem
{
	public interface IStat
	{
		StatSheet Sheet { get; set; }
		StatType Type { get; }
		float BaseValue { get; set; }
		UnityEvent<float> OnBaseValueChanged { get; }
		float Value { get; }
		UnityEvent<float> OnValueChanged { get; }
	}
}

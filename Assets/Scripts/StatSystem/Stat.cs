using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Stat : IStat
{
	private bool dirty = true;

	[SerializeField]
	protected float baseValue = 0;

	public virtual float BaseValue
	{
		get => baseValue;
		set
		{
			float old = baseValue;
			baseValue = value;
			dirty = true;
			onBaseValueChanged.Invoke(new ValueChangedEvent(old, value));
		}
	}

	public UnityEvent<ValueChangedEvent> onBaseValueChanged;

	[SerializeField]
	protected float value = 0;

	public virtual float Value
	{
		get
		{
			if (dirty)
			{
				float old = baseValue;
				value = Recalculate();
				dirty = false;
				onValueChanged.Invoke(new ValueChangedEvent(old, value));
			}

			return value;
		}
		set => this.value = value;
	}

	public UnityEvent<ValueChangedEvent> onValueChanged;

	public virtual float Recalculate()
	{
		if (!dirty)
			return value;

		return BaseValue;
	}
}

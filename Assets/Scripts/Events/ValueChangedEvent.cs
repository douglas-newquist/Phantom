using UnityEngine;

public class ValueChangedEvent : Event
{
	public float old, current;

	public float Delta => current - old;

	public float AbsDelta => Mathf.Abs(Delta);

	public ValueChangedEvent(float old, float current)
	{
		this.old = old;
		this.current = current;
	}

	public override string ToString()
	{
		return string.Format("Value changed from {0} to {1} ({2})", old, current, Delta);
	}
}

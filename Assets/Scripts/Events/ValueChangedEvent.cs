using UnityEngine;

public class ValueChangedEvent : Event
{
	public float Old { get; private set; }

	public float Current { get; private set; }

	public float Delta => Current - Old;

	public float AbsDelta => Mathf.Abs(Delta);

	public ValueChangedEvent(float old, float current)
	{
		Old = old;
		Current = current;
	}

	public override string ToString()
	{
		return string.Format("Value changed from {0} to {1} ({2})", Old, Current, Delta);
	}
}

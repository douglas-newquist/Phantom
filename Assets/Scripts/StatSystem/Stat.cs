using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	[System.Serializable]
	public class Stat : IStat
	{
		protected bool dirty = true;

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

				if (old != baseValue)
					OnBaseValueChanged.Invoke(new ValueChangedEvent(this, old, value));
			}
		}

		public UnityEvent<ValueChangedEvent> OnBaseValueChanged
		{
			get => onBaseValueChanged;
			set => onBaseValueChanged = value;
		}

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

					if (old != value)
						OnValueChanged.Invoke(new ValueChangedEvent(this, old, value));
				}

				return value;
			}
			set => this.value = value;
		}

		public UnityEvent<ValueChangedEvent> OnValueChanged
		{
			get => onValueChanged;
			set => onValueChanged = value;
		}

		[SerializeField]
		protected UnityEvent<ValueChangedEvent> onBaseValueChanged, onValueChanged;

		protected List<IModifier> modifiers = new List<IModifier>();

		public Stat()
		{
			onBaseValueChanged = new UnityEvent<ValueChangedEvent>();
			onValueChanged = new UnityEvent<ValueChangedEvent>();
		}

		public virtual float Recalculate()
		{
			return modifiers.ApplyModifiers(BaseValue);
		}

		public void AddModifier(IModifier modifier)
		{
			modifiers.Add(modifier);
			dirty = true;
		}

		public bool RemoveModifier(IModifier modifier)
		{
			bool removed = modifiers.Remove(modifier);
			dirty = dirty || removed;
			return removed;
		}

		public void RemoveModifiersFromSource(object source)
		{
			for (int i = modifiers.Count - 1; i >= 0; i--)
			{
				if (modifiers[i].Source == source)
				{
					modifiers.RemoveAt(i);
					dirty = true;
				}
			}
		}
	}
}

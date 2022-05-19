using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	public class StatSheet
	{
		private Dictionary<StatSO, Stat> stats = new Dictionary<StatSO, Stat>();
	}
	[System.Serializable]
	public class Stat
	{
		public StatSO Type { get; protected set; }

		public bool Dirty { get; protected set; } = true;

		[SerializeField]
		protected float baseValue = 0;

		public virtual float BaseValue
		{
			get => baseValue;
			set
			{
				float old = baseValue;
				baseValue = value;
				Dirty = true;

				if (old != baseValue)
					OnBaseValueChanged.Invoke(new ValueChangedEvent(this, old, value));
			}
		}
		[SerializeField]
		protected UnityEvent<ValueChangedEvent> onBaseValueChanged;

		public UnityEvent<ValueChangedEvent> OnBaseValueChanged => onBaseValueChanged;

		[SerializeField]
		protected float value = 0;

		public virtual float Value
		{
			get
			{
				if (Dirty)
					Recalculate();

				return value;
			}
		}

		[SerializeField]
		protected UnityEvent<ValueChangedEvent> onValueChanged;

		public UnityEvent<ValueChangedEvent> OnValueChanged => onValueChanged;

		[SerializeField]
		protected List<IModifier> modifiers = new List<IModifier>();


		public Stat()
		{
			modifiers = new List<IModifier>();
			onValueChanged = new UnityEvent<ValueChangedEvent>();
			onBaseValueChanged = new UnityEvent<ValueChangedEvent>();
		}

		public Stat(StatSO type, float baseValue) : this()
		{
			this.baseValue = baseValue;
			this.value = baseValue;
			Type = type;
		}

		public virtual void Recalculate()
		{
			if (!Dirty) return;

			float old = value;
			value = modifiers.ApplyModifiers(BaseValue);
			Dirty = false;

			if (old != value)
				OnValueChanged.Invoke(new ValueChangedEvent(this, old, value));
		}

		public void AddModifier(IModifier modifier)
		{
			modifiers.Add(modifier);
			Dirty = true;
		}

		public bool RemoveModifier(IModifier modifier)
		{
			bool removed = modifiers.Remove(modifier);
			Dirty = Dirty || removed;
			return removed;
		}

		public void RemoveModifiersFromSource(object source)
		{
			for (int i = modifiers.Count - 1; i >= 0; i--)
			{
				if (modifiers[i].Source == source)
				{
					modifiers.RemoveAt(i);
					Dirty = true;
				}
			}
		}
	}
}

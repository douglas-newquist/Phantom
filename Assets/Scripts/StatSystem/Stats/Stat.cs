using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Phantom
{
	[System.Serializable]
	public class Stat : IModifiableStat
	{
		/// <summary>
		/// The stat sheet this stat is associated with
		/// </summary>
		public StatSheet Sheet { get; set; }

		/// <summary>
		/// What type of stat this stat is
		/// </summary>
		public StatType Type { get; protected set; }

		public bool Dirty { get; protected set; } = true;

		[SerializeField]
		protected float baseValue = 0;

		/// <summary>
		/// The starting value of this stat before modifiers
		/// </summary>
		public virtual float BaseValue
		{
			get => baseValue;
			set
			{
				float old = baseValue;

				if (Type != null)
					baseValue = Type.limits.Clamp(value);
				else
					baseValue = value;

				MarkDirty();

				if (old != baseValue)
					OnBaseValueChanged.Invoke(new ValueChangedEvent(this, old, value));
			}
		}
		[SerializeField]
		protected UnityEvent<ValueChangedEvent> onBaseValueChanged;

		public UnityEvent<ValueChangedEvent> OnBaseValueChanged => onBaseValueChanged;

		[SerializeField]
		protected float value = 0;

		/// <summary>
		/// The current value of this stat after modifiers
		/// </summary>
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

		public Stat(StatType type, float baseValue) : this()
		{
			this.baseValue = baseValue;
			this.value = baseValue;
			Type = type;
		}

		public override string ToString()
		{
			return Type.name + " " + Value + " (" + BaseValue + ")";
		}

		/// <summary>
		/// Recalculates the current value for this stat
		/// </summary>
		public virtual void Recalculate()
		{
			float old = value;
			value = modifiers.ApplyModifiers(Sheet, BaseValue);
			if (Type != null)
				value = Type.limits.Clamp(value);
			Dirty = false;

			if (old != value)
				OnValueChanged.Invoke(new ValueChangedEvent(this, old, value));
		}

		/// <summary>
		/// Adds a modifier to this stat
		/// </summary>
		public void AddModifier(IModifier modifier)
		{
			if (modifier == null) return;
			modifiers.Add(modifier);
			MarkDirty();
		}

		/// <summary>
		/// Removes a specific modifier
		/// </summary>
		/// <returns>True if modifier existed and was removed</returns>
		public bool RemoveModifier(IModifier modifier)
		{
			if (modifier == null) return false;
			bool removed = modifiers.Remove(modifier);
			MarkDirty();
			return removed;
		}

		/// <summary>
		/// Removes all modifiers from a specific source
		/// </summary>
		public void RemoveModifiersFromSource(object source)
		{
			for (int i = modifiers.Count - 1; i >= 0; i--)
				if (modifiers[i].Source == source)
				{
					modifiers.RemoveAt(i);
					MarkDirty();
				}
		}

		/// <summary>
		/// Marks this stat's current value as dirty
		/// </summary>
		public void MarkDirty()
		{
			Dirty = true;
			Recalculate();
		}
	}
}

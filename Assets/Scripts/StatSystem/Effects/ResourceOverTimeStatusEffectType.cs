using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.StatusEffect + "Resource Over Time")]
	public class ResourceOverTimeStatusEffectType : StatusEffectType
	{
		[SerializeField]
		private ResourceStatType resource;

		public ResourceStatType Resource => resource;

		[SerializeField]
		[Tooltip("If defined, will use the given stat as the base amount")]
		private StatType referenceStat;

		public StatType ReferenceStat => referenceStat;

		[SerializeField]
		private float amount;

		public float Amount => amount;

		public override IStatusEffect Apply(StatSheet statSheet, object source)
		{
			var effect = new ResourceOverTimeStatusEffect(this, source);
			effect.Apply(statSheet);
			return effect;
		}
	}
}

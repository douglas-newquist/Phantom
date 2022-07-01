using System.Collections;
using UnityEngine;

namespace Phantom.StatSystem
{
	public class ResourceOverTimeStatusEffect : StatusEffect
	{
		public const float delay = 0.2f;

		ResourceOverTimeStatusEffectType effect;
		IResourceStat resource;
		IStat reference;

		float end;

		public ResourceOverTimeStatusEffect(StatusEffectType type, object source) : base(type, source)
		{
		}

		protected override bool PreEffect(StatSheet statSheet)
		{
			effect = (ResourceOverTimeStatusEffectType)Type;

			if (!statSheet.TryGetStat<IResourceStat>(effect.Resource, out resource))
				return false;
			statSheet.TryGetStat(effect.ReferenceStat, out reference);

			float duration = Type.Duration.Random;
			end = duration < 0 ? -1 : Time.time + duration;

			return base.PreEffect(statSheet);
		}

		protected override IEnumerator DoEffect(StatSheet statSheet)
		{
			while (end < 0 || Time.time < end)
			{
				yield return new WaitForSeconds(delay);

				if (reference != null)
					resource.Current += effect.Amount * delay * reference.Value;
				else
					resource.Current += effect.Amount * delay;
			}
		}
	}
}

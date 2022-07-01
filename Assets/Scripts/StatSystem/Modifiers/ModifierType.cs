using UnityEngine;

namespace Phantom.StatSystem
{
	public abstract class ModifierType : ScriptableObject
	{
		public const string CreateMenu = "Game/Stat System/Modifier/";

		[Range(0, 100)]
		public int order = 0;

		public bool stacks = true;

		public abstract IModifier Create(object source, float magnitude);

		public virtual IModifier Create(float magnitude) => Create(null, magnitude);
	}
}

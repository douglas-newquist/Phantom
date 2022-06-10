using UnityEngine;

namespace Phantom
{
	public abstract class ModifierSO : ScriptableObject
	{
		[Range(0, 100)]
		public int order = 0;

		public bool stacks = true;

		public abstract IModifier Create(object source, float magnitude);

		public virtual IModifier Create(float magnitude) => Create(this, magnitude);
	}
}

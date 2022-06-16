using UnityEngine;

namespace Phantom
{
	public abstract class NameGenerator : ScriptableObject
	{
		[MinMax(0, 16)]
		public IntRange repeat = new IntRange(1, 1);

		public string prefix = "", postfix = "";

		public abstract string ApplyOnce(string name);

		public virtual string Apply(string name)
		{
			for (int repeats = repeat.Random; repeats > 0; repeats--)
				name = ApplyOnce(name);

			return name;
		}

		public virtual string Create()
		{
			return Apply("");
		}
	}
}

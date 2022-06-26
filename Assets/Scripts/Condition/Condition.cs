using UnityEngine;

namespace Phantom
{
	public abstract class Condition : ScriptableObject
	{
		public abstract bool Satisfied(GameObject gameObject);
	}
	public abstract class Action : ScriptableObject
	{
		public Condition[] conditions;

		public abstract Coroutine Perform(GameObject gameObject);
	}
}

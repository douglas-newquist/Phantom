using UnityEngine;

namespace Phantom
{
	public abstract class Condition : ScriptableObject
	{
		public abstract bool Satisfied(GameObject gameObject);
	}
}

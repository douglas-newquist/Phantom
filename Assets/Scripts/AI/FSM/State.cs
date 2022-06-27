using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Game + "Finite State")]
	public class State : ScriptableObject
	{
		public int priority;

		public Condition[] preConditions;

		public State[] transitions;
	}
}

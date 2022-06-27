using UnityEngine;

namespace Phantom
{
	public abstract class Action : MonoBehaviour
	{
		[SerializeField]
		private int priority;

		public int Priority
		{
			get => priority;
			set => priority = value;
		}

		public Condition[] conditions;
	}
}

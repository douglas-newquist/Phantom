using System.Collections;
using UnityEngine;

namespace Phantom
{
	public class WanderAction : Action, IAction
	{
		IMover mover;
		ILooker looker;

		public Vector2 origin;
		public Vector2 target;
		[MinMax(0f, Level.TotalSizeLimit)]
		public FloatRange wanderDistance;

		public override bool IsPossible => base.IsPossible && mover != null;

		private void Start()
		{
			origin = transform.position;
			mover = GetComponent<IMover>();
			looker = GetComponent<ILooker>();
		}

		protected override IEnumerator DoAction()
		{
			while (true)
			{
				throw new System.NotImplementedException();
			}
		}
	}
}

using System.Collections;
using UnityEngine;

namespace Phantom
{
	public class PatrolAction : Action
	{
		public Vector2[] points;

		public bool loopsBackToStart = false;

		protected override IEnumerator DoAction()
		{
			throw new System.NotImplementedException();
		}
	}
}

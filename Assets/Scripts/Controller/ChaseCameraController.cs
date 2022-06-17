using UnityEngine;
using System.Collections;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Controller/Chase Camera")]
	public class ChaseCameraController : Controller<Camera>
	{
		public float snapDistance = 0.025f;

		public override IEnumerator Control(Camera controllable)
		{
			while (controllable != null)
			{
				yield return new WaitForEndOfFrame();
			}
		}
	}
}

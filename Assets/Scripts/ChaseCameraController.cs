using UnityEngine;
using System.Collections;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Controller/Chase Camera")]
	public class ChaseCameraController : Controller
	{
		public float snapDistance = 0.025f;

		public override IEnumerator Control(Controllable controllable)
		{
			Camera camera = controllable.GetComponent<Camera>();

			while (true)
			{
				yield return new WaitForEndOfFrame();
			}
		}
	}
}

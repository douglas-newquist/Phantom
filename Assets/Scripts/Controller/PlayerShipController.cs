using System.Collections;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Controller + "Player Ship Controller")]
	public class PlayerShipController : ShipController
	{
		public Reference thrustReference;

		public override IEnumerator Control(Ship controllable)
		{
			while (controllable != null)
			{
				if (Input.GetKeyDown(KeyCode.R))
				{
					switch (thrustReference)
					{
						case Reference.Relative:
							thrustReference = Reference.Absolute;
							break;

						case Reference.Absolute:
							thrustReference = Reference.Relative;
							break;
					}
				}

				var x = Input.GetAxis("Horizontal");
				var y = Input.GetAxis("Vertical");
				var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mouse.z = 0;

				if (Input.GetKey(KeyCode.X))
					controllable.Stop();
				else
					controllable.Move(new Vector2(x, y), thrustReference);

				controllable.Look(mouse, Reference.Absolute);

				//				if (target != null)
				//					Turrets.Aim(target, Input.GetMouseButton(0));
				//				else
				//					Turrets.Aim(mouse, Reference.Absolute, Input.GetMouseButton(0));
				yield return null;
			}
		}
	}
}

using System.Collections;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Controller + "Player Ship Controller")]
	public class PlayerShipController : Controller
	{
		public Reference thrustReference;

		public override IEnumerator Control(GameObject gameObject)
		{
			IMover movable = gameObject.GetComponent<IMover>();
			ILooker lookable = gameObject.GetComponent<ILooker>();
			IWeaponSystem weaponSystem = gameObject.GetComponent<IWeaponSystem>();
			Vector2 target = gameObject.transform.position;

			while (gameObject != null)
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
				var direction = new Vector2(x, y);
				var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mouse.z = 0;

				if (Input.GetMouseButton(1))
					target = mouse;

				if (Input.GetKey(KeyCode.X))
					movable.Brake();
				else if (direction != Vector2.zero)
					movable.Move(direction, thrustReference);
				else
					movable.MoveTo(target);

				if (lookable != null)
					lookable.Look(mouse, Reference.Absolute);

				//				if (target != null)
				//					Turrets.Aim(target, Input.GetMouseButton(0));
				//				else
				if (Input.GetMouseButton(0))
					weaponSystem.Fire(mouse, Reference.Absolute);
				else
					weaponSystem.Aim(mouse, Reference.Absolute);
				yield return null;
			}
		}
	}
}

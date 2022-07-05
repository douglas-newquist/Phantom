using System.Collections;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Controller + "Player Ship Controller")]
	public class PlayerShipController : Controller
	{
		public Reference thrustReference;

		enum Mode
		{
			Normal,
			Brake,
			MoveTo,
		}

		public override IEnumerator Control(GameObject gameObject)
		{
			IMover movable = gameObject.GetComponent<IMover>();
			ILooker lookable = gameObject.GetComponent<ILooker>();
			IWeaponSystem weaponSystem = gameObject.GetComponent<IWeaponSystem>();
			Vector2 target = gameObject.transform.position;

			Mode mode = Mode.Normal;

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

				if (Input.GetKey(KeyCode.X))
				{
					mode = Mode.Brake;
				}
				else if (Input.GetMouseButton(1))
				{
					mode = Mode.MoveTo;
					target = mouse;
				}
				else if (direction != Vector2.zero)
				{
					mode = Mode.Normal;
				}

				switch (mode)
				{
					case Mode.Brake:
						movable.Brake();
						lookable.Look(mouse, Reference.Absolute);
						break;

					case Mode.Normal:
						movable.MoveRelative(direction, thrustReference);
						lookable.Look(mouse, Reference.Absolute);
						break;

					case Mode.MoveTo:
						movable.MoveTo(target);
						lookable.Look(target, Reference.Absolute);
						break;
				}

				if (Input.GetMouseButton(0))
					weaponSystem.Fire(mouse, Reference.Absolute);
				else
					weaponSystem.Aim(mouse, Reference.Absolute);

				yield return null;
			}
		}
	}
}

using System.Collections;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu + "AI Ship Controller")]
	public class GoapAIShipController : Controller
	{
		public override IEnumerator Control(GameObject gameObject)
		{
			IMover movable = gameObject.GetComponent<IMover>();
			ILooker lookable = gameObject.GetComponent<ILooker>();
			IWeaponSystem weaponSystem = gameObject.GetComponent<IWeaponSystem>();
			IEntity entity = gameObject.GetComponent<IEntity>();
			IAction[] actions = gameObject.GetComponentsInChildren<IAction>();
			Debug.Log(actions.Length);

			while (entity.IsAlive)
			{
				var action = actions[Random.Range(0, actions.Length)];
				action.Perform();
				yield return new WaitUntil(() => action.Completed);
			}
		}
	}
}

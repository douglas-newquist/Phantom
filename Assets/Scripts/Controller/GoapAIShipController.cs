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
			GoapPlanner planner = gameObject.GetComponent<GoapPlanner>();
			planner.ScanActions();
			Debug.Log(actions.Length);

			while (entity.IsAlive)
			{
				var goal = new WorldStates();
				if (planner.Plan(goal) == false)
				{
					yield return new WaitForSeconds(5);
					continue;
				}

				if (planner.Execute() == false)
				{
					yield return new WaitForSeconds(5);
					continue;
				}

				yield return new WaitUntil(() => !planner.Executing);
			}
		}
	}
}

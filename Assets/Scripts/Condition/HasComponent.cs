using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Condition + "Has Component")]
	public class HasComponent : Condition
	{
		public string type;

		public override bool Satisfied(GameObject gameObject)
		{
			return gameObject.GetComponent(type) != null;
		}
	}
}

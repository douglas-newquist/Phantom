using System.Collections;
using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Controller/Player")]
	public class PlayerController : Controller
	{
		public StatSO speedStat;

		public override IEnumerator Control(Controllable controllable)
		{
			Entity entity = controllable.GetComponent<Entity>();
			var character = controllable as ControllableCharacter;

			var speed = entity.Stats.GetStat(speedStat);
			speed.AddModifier(new AdditiveModifier(this, 0, true, 5));

			if (entity == null)
			{
				Debug.Log("PlayerController must control an Entity");
				yield break;
			}

			while (true)
			{
				float x = Input.GetAxis("Horizontal");
				float y = Input.GetAxis("Vertical");
				var change = new Vector3(x, y, 0) * Time.deltaTime;
				entity.transform.position += change * entity.Stats.GetValue(speedStat);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}

using UnityEngine;
using System.Collections;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Controller/Player")]
	public class PlayerController : Controller
	{
		public override IEnumerator Control(Controllable controllable)
		{
			Entity entity = controllable.GetComponent<Entity>();

			if (entity == null)
			{
				Debug.Log("PlayerController must control an Entity");
				yield break;
			}

			while (true)
			{
				float x = Input.GetAxis("Horizontal");
				float y = Input.GetAxis("Vertical");
				entity.transform.position += new Vector3(x, y, 0) * Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}
	}
}

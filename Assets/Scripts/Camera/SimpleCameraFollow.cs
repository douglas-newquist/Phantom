using UnityEngine;

namespace Phantom
{
	public class SimpleCameraFollow : MonoBehaviour
	{
		public GameObject target;

		[Range(0.1f, GameManager.SpeedLimit)]
		public float speed = 1;

		private void FixedUpdate()
		{
			if (target != null)
			{
				var pos = target.transform.position;
				pos.z = transform.position.z;
				transform.position = Vector3.Lerp(transform.position, pos, Time.fixedDeltaTime * speed);
			}
		}
	}
}

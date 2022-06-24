using UnityEngine;
using UnityEngine.Events;

namespace Phantom
{
	[DisallowMultipleComponent]
	public class SimpleFollowCameraExtension : CameraExtension
	{
		[SerializeField]
		GameObject target;

		[SerializeField]
		Rigidbody2D body;

		[Range(0.1f, GameManager.SpeedLimit)]
		public float speed = 1;

		public UnityEvent<GameObject> OnTargetChanged;

		private void FixedUpdate()
		{
			if (target == null) return;

			Vector3 pos = target.transform.position;
			pos.z = transform.position.z;

			if (body != null)
			{
				pos.x += body.velocity.x * Time.fixedDeltaTime;
				pos.y += body.velocity.y * Time.fixedDeltaTime;
			}

			transform.position = Vector3.Lerp(transform.position, pos, Time.fixedDeltaTime * speed);
		}

		public void SetTarget(GameObject obj)
		{
			target = obj;
			body = obj != null ? obj.GetComponent<Rigidbody2D>() : null;
			OnTargetChanged.Invoke(obj);
		}
	}
}

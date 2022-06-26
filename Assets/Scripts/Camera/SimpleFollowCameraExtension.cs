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

		public Vector3 TargetPosition
		{
			get
			{
				if (target == null) return transform.position;

				Vector3 pos = target.transform.position;
				pos.z = transform.position.z;
				return pos;
			}
		}

		public Vector3 PredictedTargetPosition
		{
			get
			{
				Vector3 pos = TargetPosition;

				if (body != null)
				{
					pos.x += body.velocity.x * Time.fixedDeltaTime;
					pos.y += body.velocity.y * Time.fixedDeltaTime;
				}

				return pos;
			}
		}

		private Vector3 position;

		private void Start()
		{
			position = transform.position;
		}

		private void FixedUpdate()
		{
			if (target == null) return;

			position = Vector3.Lerp(position, PredictedTargetPosition, Time.fixedDeltaTime * speed);

			transform.position = position;
		}

		public void SetTarget(GameObject obj)
		{
			target = obj;
			body = obj != null ? obj.GetComponent<Rigidbody2D>() : null;
			OnTargetChanged.Invoke(obj);
		}
	}
}

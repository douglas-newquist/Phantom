using UnityEngine;

namespace Phantom
{
	public class GyroController : MonoBehaviour
	{
		public Rigidbody2D body => GetComponent<Rigidbody2D>();

		public Gyro[] Gyros => GetComponentsInChildren<Gyro>();

		public void Look(Vector3 vector, Reference mode)
		{
			switch (mode)
			{
				case Reference.Absolute:
					vector -= transform.position;
					break;
			}

			body.transform.up = vector;
		}
	}
}

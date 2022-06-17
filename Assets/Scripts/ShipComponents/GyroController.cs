using UnityEngine;

namespace Phantom
{
	public class GyroController : MonoBehaviour, ILookable
	{
		public Rigidbody2D body => GetComponent<Rigidbody2D>();

		public Gyro[] Gyros => GetComponentsInChildren<Gyro>();

		public void Look(Vector2 vector, Reference mode)
		{
			switch (mode)
			{
				case Reference.Absolute:
					vector -= (Vector2)transform.position;
					break;
			}

			body.transform.up = vector;
		}
	}
}

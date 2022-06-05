using UnityEngine;

namespace Game
{
	public class GyroController : MonoBehaviour
	{
		public Rigidbody2D body;

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

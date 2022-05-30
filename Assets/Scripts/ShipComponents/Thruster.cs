using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public interface IThruster
	{

	}
	public class Thruster : MonoBehaviour, IThruster
	{
		public Rigidbody2D body;
		public StatSheet statSheet;
		public StatSO thrustStat;
		public float multiplier = 1;

		private Vector3 force;

		public void Move(Vector3 vector, Reference mode)
		{
			switch (mode)
			{
				case Reference.Absolute:
					break;

				case Reference.Relative:
					break;
			}

			force += vector * multiplier * Time.deltaTime;
		}

		// Update is called once per frame
		void Update()
		{
			var x = Input.GetAxis("Horizontal");
			var y = Input.GetAxis("Vertical");
			Move(new Vector2(x, y), Reference.Relative);
		}

		private void FixedUpdate()
		{
			if (force != Vector3.zero)
			{
				body.AddRelativeForce(force, ForceMode2D.Impulse);
				force = Vector3.zero;
			}
		}
	}
}

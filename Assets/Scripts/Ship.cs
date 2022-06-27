using UnityEngine;
using Phantom.StatSystem;

namespace Phantom
{
	[RequireComponent(typeof(StatSheet))]
	public class Ship : Entity
	{
		public Rigidbody2D body => GetComponent<Rigidbody2D>();

		public ThrusterController thrusters;

		[SerializeField]
		private GyroController gyros;

		public GyroController Gyros => gyros;

		[SerializeField]
		private TurretController turrets;

		public TurretController Turrets => turrets;

		public Rigidbody2D target;

		[SerializeField]
		private StatType massStat;

		public Controller controller;

		private void Start()
		{
			var mass = Stats.GetStat(massStat);
			body.mass = mass.Value;
			// TODO Spawning might run this multiple times
			mass.OnValueChanged.AddListener(stat => body.mass = stat.Current);
			StartCoroutine(controller.Control(gameObject));
		}

		public void Move(Vector2 vector, Reference mode)
		{
			thrusters.Move(vector, mode);
		}

		public void Look(Vector2 vector, Reference mode)
		{
			gyros.Look(vector, mode);
		}

		public void Stop()
		{
			thrusters.Brake();
		}
	}
}

using Phantom.StatSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	[RequireComponent(typeof(StatSheet))]
	public class Ship : Entity
	{
		public Rigidbody2D body => GetComponent<Rigidbody2D>();

		[SerializeField]
		private StatType massStat;

		public Controller controller;

		private void Awake()
		{
			GetComponent<StatSheet>().Clear();
			var tilemap = GetComponentInChildren<Tilemap>();
			tilemap.RefreshAllTiles();
		}

		private void Start()
		{
			var mass = Stats.GetStat(massStat);
			body.mass = mass.Value;
			// TODO Spawning might run this multiple times
			mass.OnValueChanged.AddListener(stat => body.mass = stat.Current);
			StartCoroutine(controller.Control(gameObject));
		}
	}
}

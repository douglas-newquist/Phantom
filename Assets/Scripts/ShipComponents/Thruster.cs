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
		public StatSheet statSheet;
		public StatSO thrustStat;
		public float multiplier = 1;

		// Update is called once per frame
		void Update()
		{

		}
	}
}

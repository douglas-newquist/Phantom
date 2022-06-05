using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Parts/Part")]
	public class ShipPartSO : ScriptableObject
	{
		public GameObject prefab;

		[Range(1, 16)]
		public int width = 1, height = 1;

		public TileShape placement = TileShape.Full;

		public List<StatPair> baseStats;
	}
}

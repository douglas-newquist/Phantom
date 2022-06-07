using UnityEngine;
namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "Hull")]
	public class TileMapSO : ScriptableObject
	{
		public TileWeights weights = new TileWeights();

		public StatPair[] stats;
	}
}

using UnityEngine;
namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "Hull")]
	public class TileMapSO : ScriptableObject
	{
		public TileWeights weights = new TileWeights();

		public TileMapTexture texture;

		public StatPair[] stats;

		public Sprite GetSprite(TileMap map)
		{
			return texture.DrawSprite(map);
		}
	}
}

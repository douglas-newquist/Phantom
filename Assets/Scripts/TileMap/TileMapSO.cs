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

		public void ApplyStats(StatSheet statSheet, TileMap map)
		{
			var counts = map.Itemize();
			foreach (var count in counts)
			{
				var weight = weights.GetWeight(count.Key) * count.Value;
				foreach (var stat in stats)
				{
					statSheet.GetStat(stat.stat).BaseValue += stat.baseValue * weight;
				}
			}
		}
	}
}

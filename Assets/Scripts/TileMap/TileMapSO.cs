using UnityEngine;
namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "Hull")]
	public class TileMapSO : ScriptableObject
	{
		public TileWeights weights = new TileWeights();

		public TileMapTexture texture;

		public StatPair[] stats;

		public StatPair[] GetHullStats(TileMap map)
		{
			var baseStats = new StatPair[stats.Length];
			var counts = map.Itemize();

			for (int i = 0; i < stats.Length; i++)
			{

			}
			foreach (var count in counts)
			{
				var weight = weights.GetWeight(count.Key) * count.Value;
				foreach (var stat in stats)
				{
				}
			}

			return baseStats;
		}

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

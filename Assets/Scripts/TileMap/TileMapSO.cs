using UnityEngine;
using UnityEngine.Tilemaps;
using Phantom.StatSystem;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "Hull")]
	public class TileMapSO : ScriptableObject
	{
		public VertexTileShapePair<float> weights;

		public TileMapTexture texture;

		public VertexTiles hullTiles;

		public StatValue[] stats;

		public Modifier[] modifiers;

		public StatValue[] GetHullStats(VertexTileMap map)
		{
			var baseStats = new StatValue[stats.Length];
			var counts = map.Itemize();

			for (int i = 0; i < stats.Length; i++)
			{

			}
			foreach (var count in counts)
			{
				var weight = weights.Get(count.Key) * count.Value;
				foreach (var stat in stats)
				{
				}
			}

			return baseStats;
		}

		public Sprite GetSprite(VertexTileMap map)
		{
			return texture.DrawSprite(map);
		}

		public void ApplyStats(StatSheet statSheet, VertexTileMap map)
		{
			var counts = map.Itemize();
			foreach (var count in counts)
			{
				var weight = weights.Get(count.Key) * count.Value;
				foreach (var stat in stats)
				{
					statSheet.GetStat(stat.stat).BaseValue += stat.baseValue * weight;
				}
			}
		}
	}
}

using System;
using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Defines specific information on how to construct a ship
	/// </summary>
	[System.Serializable]
	public class ShipDesign
	{
		public const int SizeLimit = 32;

		[SerializeField]
		private string name;

		public string Name
		{
			get => name;
			set => name = value;
		}

		[SerializeField]
		private TileLayerMap tileLayerMap;

		public TileLayerMap TileLayerMap
		{
			get => tileLayerMap;
			set => tileLayerMap = value;
		}

		[SerializeField]
		private TileMapSO hullType;

		public TileMapSO HullType
		{
			get => hullType;
			set
			{
				hullType = value;
				TileLayerMap.VertexTileTiles = hullType.hullTiles;
			}
		}

		public int Width => TileLayerMap.Width;

		public int Height => TileLayerMap.Height;

		public RectInt Bounds => TileLayerMap.Tiles.BoundingBox;

		public ShipDesign(int width, int height)
		{
			TileLayerMap = new TileLayerMap(width, height);
		}

		public ShipDesign(ShipDesign design)
		{
			TileLayerMap = new TileLayerMap(design.TileLayerMap);
			Name = design.Name;
			hullType = design.HullType;
		}
	}
}

using System;
using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Defines specific information on how to construct a ship
	/// </summary>
	[System.Serializable]
	public class ShipDesign : TileObjectMap
	{
		[SerializeField]
		private string name;

		public string Name { get => name; set => name = value; }

		public TileMapSO hullType;


		public ShipDesign(int width, int height) : base(width, height)
		{
		}

		public ShipDesign(TileObjectMap map) : base(map) { }

		public ShipDesign(ShipDesign design) : base(design)
		{
			hullType = design.hullType;
		}

		public override IGrid2D<Tuple<VertexTile, TileObject>> Clone()
		{
			return new ShipDesign(this);
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class LevelDesign : TileObjectMap
	{
		public TileMapTexture tileMapTexture;

		public LevelDesign(int width, int height) : base(width, height)
		{
		}

		public LevelDesign(TileObjectMap map) : base(map) { }

		public LevelDesign(LevelDesign level) : base(level)
		{
		}

		public override IGrid2D<Tuple<VertexTile, TileObject>> Clone()
		{
			return new LevelDesign(this);
		}
	}
}

namespace Phantom
{
	public partial class TileLayerMap
	{
		/// <summary>
		/// Stores a tile grid/z coordinate pair
		/// </summary>
		[System.Serializable]
		private class TileLayer
		{
			/// <summary>
			/// Z coordinate of this layer
			/// </summary>
			public int z;

			/// <summary>
			/// The tiles on this layer
			/// </summary>
			public Grid2D<TileObject> tiles;

			public TileLayer(int z, Grid2D<TileObject> tiles)
			{
				this.z = z;
				this.tiles = tiles;
			}

			public TileLayer(TileLayer layer)
			{
				z = layer.z;
				tiles = new Grid2D<TileObject>(layer.tiles);
			}
		}
	}
}

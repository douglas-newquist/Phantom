namespace Phantom
{
	public abstract class TileLayerMapGenerator : Generator<TileLayerMap>
	{
		public override TileLayerMap Apply(TileLayerMap design)
		{
			return Apply(design, design.Bounds);
		}

		public override TileLayerMap Create(int width, int height)
		{
			var design = new TileLayerMap(width, height);
			return Apply(design);
		}
	}
}

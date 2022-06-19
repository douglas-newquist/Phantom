using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Selector")]
	public class SelectorGridGen : GridGen
	{
		public WeightedList<GridGen> choices;

		public override VertexTileMap ApplyOnce(VertexTileMap grid, RectInt area)
		{
			var generator = choices.GetRandom();
			if (generator != null)
				return generator.Apply(grid, area);
			return grid;
		}
	}
}

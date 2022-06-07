using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Generation/Tiles/Selector")]
	public class SelectorGridGen : GridGen
	{
		public WeightedList<GridGen> choices;

		protected override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			var gen = choices.GetRandom();
			if (gen != null)
				return gen.Apply(grid, area);
			return grid;
		}
	}
}

using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Generation/Tiles/Sequence")]
	public class SequenceGridGen : GridGen<int>
	{
		public GridGen<int> mask;

		public GridGen<int>[] steps;

		protected override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			var result = grid;

			Grid2D<int> gridMask = null;
			if (mask != null)
				gridMask = mask.Create(area.width, area.height);

			foreach (var step in steps)
			{
				result = step.Apply(result, area);
			}

			return result;
		}
	}
}

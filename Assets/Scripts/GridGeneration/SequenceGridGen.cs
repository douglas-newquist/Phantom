using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Generation/Tiles/Sequence")]
	public class SequenceGridGen : GridGen<int>
	{
		public GridGen<int>[] steps;

		public override Grid2D<int> Apply(Grid2D<int> grid, RectInt area)
		{
			var result = grid;

			foreach (var step in steps)
				result = step.Apply(result, area);

			return result;
		}
	}
}

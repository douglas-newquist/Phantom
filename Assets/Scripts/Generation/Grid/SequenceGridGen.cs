using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Generators/Tiles/Sequence")]
	public class SequenceGridGen : GridGen
	{
		public GridGen[] steps;

		protected override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			var result = grid;

			foreach (var step in steps)
				if (step != null)
					result = step.Apply(result, area);

			return result;
		}
	}
}

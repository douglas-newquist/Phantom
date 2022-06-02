using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Generation/Tiles/Marching Squares")]
	public class MarchingSquaresGridGen : GridGen<int>
	{
		public IntRange n;

		public override Grid2D<int> Apply(Grid2D<int> grid, RectInt area)
		{
			var result = new Grid2D<int>(grid);

			for (int x = area.xMin; x < area.xMax; x++)
				for (int y = area.yMin; y < area.yMax; y++)
				{

				}

			return result;
		}
	}
}

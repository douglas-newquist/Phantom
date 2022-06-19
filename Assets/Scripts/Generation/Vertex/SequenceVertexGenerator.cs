using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Sequence")]
	public class SequenceVertexGenerator : VertexGenerator
	{
		public VertexGenerator[] steps;

		public override VertexTileMap ApplyOnce(VertexTileMap grid, RectInt area)
		{
			var result = grid;

			foreach (var step in steps)
				if (step != null)
					result = step.Apply(result, area);

			return result;
		}
	}
}
using UnityEngine;

namespace Phantom
{
	[DisallowMultipleComponent]
	public class WorldBoundsClampCameraExtension : CameraExtension
	{
		private void Update()
		{
			var bounds = GameManager.WorldBounds;
			var pos = WorldMin;

			pos.x = Mathf.Clamp(pos.x, bounds.xMin, bounds.xMax);
			pos.y = Mathf.Clamp(pos.y, bounds.yMin, bounds.yMax);

			WorldMin = pos;

			pos = WorldMax;

			pos.x = Mathf.Clamp(pos.x, bounds.xMin, bounds.xMax);
			pos.y = Mathf.Clamp(pos.y, bounds.yMin, bounds.yMax);

			WorldMax = pos;
		}
	}
}

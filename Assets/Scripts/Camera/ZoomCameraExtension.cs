using UnityEngine;

namespace Phantom
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	public class ZoomCameraExtension : CameraExtension
	{
		[SerializeField]
		[MinMax(1, 1000)]
		private FloatRange zoomRange = new FloatRange(100, 300);

		public FloatRange ZoomRange
		{
			get => zoomRange;
			set
			{
				zoomRange = value;
				ZoomLevel = zoomLevel;
			}
		}

		[SerializeField]
		[Range(0f, 1f)]
		private float zoomLevel = 0.5f;

		public float ZoomLevel
		{
			get => zoomLevel;
			set
			{
				zoomLevel = Mathf.Clamp01(value);
				Camera.orthographicSize = zoomRange.FromPercentage(1f - zoomLevel);
			}
		}

		private void OnGUI()
		{
			ZoomLevel = zoomLevel;
		}
	}
}

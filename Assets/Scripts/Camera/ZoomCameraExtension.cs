using UnityEngine;
using UnityEngine.Events;

namespace Phantom
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	public class ZoomCameraExtension : CameraExtension
	{
		[SerializeField]
		[MinMax(1, 5000)]
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
				var old = zoomLevel;
				zoomLevel = Mathf.Clamp01(value);
				Camera.orthographicSize = zoomRange.FromPercentage(1f - zoomLevel);

				if (GameManager.IsRunning && zoomLevel != old)
					OnZoomLevelChanged.Invoke(zoomLevel);
			}
		}

		[Range(0f, 1f)]
		private float zoomStep = 0.05f;

		public float ZoomStep
		{
			get => zoomStep;
			set => zoomStep = Mathf.Clamp01(value);
		}

		public UnityEvent<float> OnZoomLevelChanged;

		private void OnGUI()
		{
			ZoomLevel = zoomLevel;
		}

		public void ZoomIn()
		{
			ZoomLevel += ZoomStep;
		}

		public void ZoomOut()
		{
			ZoomLevel -= ZoomStep;
		}
	}
}

using UnityEngine;

namespace Phantom
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Camera))]
	[ExecuteInEditMode]
	public class CameraController : MonoBehaviour
	{
		public Camera Camera => GetComponent<Camera>();

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

		public Rect WorldBounds
		{
			get => new Rect(WorldMin, WorldMax - WorldMin);
		}

		public Vector3 WorldMin
		{
			get => Camera.ScreenToWorldPoint(Camera.pixelRect.min);
			set
			{
				Vector3 offset = WorldBounds.center - WorldBounds.min;
				transform.position = offset + value;
			}
		}

		public Vector3 WorldMax
		{
			get => Camera.ScreenToWorldPoint(Camera.pixelRect.max);
			set
			{
				Vector3 offset = WorldBounds.center - WorldBounds.max;
				transform.position = offset + value;
			}
		}

		private void Update()
		{
			if (GameManager.CurrentLevel != null)
			{
				var min = WorldMin;
				var bounds = GameManager.WorldBounds;

				min.x = Mathf.Clamp(min.x, bounds.xMin, bounds.xMax);
				min.y = Mathf.Clamp(min.y, bounds.yMin, bounds.yMax);

				WorldMin = min;
			}
		}

		private void OnGUI()
		{
			ZoomLevel = zoomLevel;
		}
	}
}

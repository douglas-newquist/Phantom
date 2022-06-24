using UnityEngine;

namespace Phantom
{
	[RequireComponent(typeof(Camera))]
	public abstract class CameraExtension : MonoBehaviour
	{
		private Camera _camera;

		/// <summary>
		/// Gets the attached camera
		/// </summary>
		public Camera Camera
		{
			get
			{
				if (_camera == null)
					_camera = GetComponent<Camera>();
				return _camera;
			}
		}

		public Rect WorldBounds
		{
			get => new Rect(WorldMin, WorldMax - WorldMin);
		}

		/// <summary>
		/// Gets the bottom left corner of the view port in world space
		/// </summary>
		public Vector3 WorldMin
		{
			get => Camera.ScreenToWorldPoint(Camera.pixelRect.min);
			set
			{
				Vector3 offset = WorldBounds.center - WorldBounds.min;
				transform.position = offset + value;
			}
		}

		/// <summary>
		/// Gets the to right corner of the view port in world space
		/// </summary>
		public Vector3 WorldMax
		{
			get => Camera.ScreenToWorldPoint(Camera.pixelRect.max);
			set
			{
				Vector3 offset = WorldBounds.center - WorldBounds.max;
				transform.position = offset + value;
			}
		}
	}
}

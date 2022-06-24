using UnityEngine;

namespace Phantom
{
	public class GameManager : MonoSingleton<GameManager>
	{
		public const float SpeedLimit = Level.TileSize * 5f;

		public const float ProjectileSpeedLimit = Level.TotalSizeLimit / 1f;

		public const float RotationSpeedLimit = 360 * 2;

		/// <summary>
		/// Maximum number of seconds a projectile can exist
		/// </summary>
		public const float ProjectileAgeLimit = 60 * 5;

		/// <summary>
		/// Is the game currently running
		/// </summary>
		public static bool IsRunning => Time.time > 0;

		private Level currentLevel;

		/// <summary>
		/// Gets the currently loaded level
		/// </summary>
		public static Level CurrentLevel
		{
			get => Instance.currentLevel;
			set
			{
				Instance.currentLevel = value;
			}
		}

		[SerializeField]
		private Rect noLevelWorldBounds;

		/// <summary>
		/// Gets the total playable area
		/// </summary>
		public static Rect WorldBounds
		{
			get
			{
				if (CurrentLevel == null)
					return Instance.noLevelWorldBounds;
				return CurrentLevel.WorldBounds;
			}
		}

		protected override void OnFirstRun()
		{
			//DontDestroyOnLoad(gameObject);
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(WorldBounds.center, WorldBounds.size);
		}
	}
}

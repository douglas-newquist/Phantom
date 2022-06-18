namespace Phantom
{
	public class GameManager : MonoSingleton<GameManager>
	{
		public const float SpeedLimit = Level.TotalSizeLimit / 1f;

		public const float RotationSpeedLimit = 360 * 2;

		/// <summary>
		/// Maximum number of seconds a projectile can exist
		/// </summary>
		public const float ProjectileAgeLimit = 60 * 5;

		protected override void OnFirstRun()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}

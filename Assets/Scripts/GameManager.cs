namespace Game
{
	public class GameManager : MonoSingleton<GameManager>
	{
		public const float SpeedLimit = 100;

		public const float RotationSpeedLimit = 360 * 2;

		protected override void OnFirstRun()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}

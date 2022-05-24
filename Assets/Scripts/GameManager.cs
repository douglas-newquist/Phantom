namespace Game
{
	public class GameManager : MonoSingleton<GameManager>
	{
		protected override void OnFirstRun()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}

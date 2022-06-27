namespace Phantom
{
	[System.Flags]
	public enum Attitude
	{
		None,
		Neutral = 1,
		Friendly = 2,
		Hostile = 4,
		Enigmatic = 8,
		AnyAttitude = ~0
	}
}

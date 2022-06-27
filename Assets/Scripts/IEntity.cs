namespace Phantom
{
	public interface IEntity
	{
		bool InCombat { get; }
		bool IsAlive { get; }
		Attitude GetAttitudeTowards(IEntity other);
	}
}

namespace Phantom.StatSystem
{
	public interface IDamageable
	{
		bool IsAlive { get; }
		bool IsDead { get; }

		void ApplyDamage(Damage damage);
	}
}

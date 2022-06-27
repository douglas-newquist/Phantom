namespace Phantom
{
	public interface IWeaponGroup : IWeapon
	{
		void Add(IWeapon weapon);

		bool Remove(IWeapon weapon);
	}
}

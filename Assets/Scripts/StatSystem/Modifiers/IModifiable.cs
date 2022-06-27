namespace Phantom
{
	public interface IModifiable
	{
		void AddModifier(IModifier modifier);
		bool RemoveModifier(IModifier modifier);
		void RemoveModifiersFromSource(object source);
	}
}

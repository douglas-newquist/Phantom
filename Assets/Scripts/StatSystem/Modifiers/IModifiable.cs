namespace Phantom.StatSystem
{
	public interface IModifiable
	{
		void AddModifier(IModifier modifier);
		bool RemoveModifier(IModifier modifier);
		void RemoveModifiersFromSource(object source);
	}
}

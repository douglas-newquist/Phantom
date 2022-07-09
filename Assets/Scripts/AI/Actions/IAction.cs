namespace Phantom
{
	public interface IAction : IReset
	{
		float Cost { get; }

		bool InRange { get; set; }

		/// <summary>
		/// Is this action currently being performed
		/// </summary>
		bool IsRunning { get; }

		bool Completed { get; }

		bool PossibleGiven(WorldStates worldStates);

		/// <summary>
		/// Start doing this action
		/// </summary>
		/// <returns>Was the action successfully started</returns>
		bool Perform();

		void StopAction();
	}
}

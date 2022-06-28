namespace Phantom
{
	public interface IAction
	{
		int Priority { get; }

		/// <summary>
		/// Is this action currently being performed
		/// </summary>
		bool IsRunning { get; }

		bool Completed { get; }

		/// <summary>
		/// Can this action be done right now
		/// </summary>
		bool IsPossible { get; }

		/// <summary>
		/// Start doing this action
		/// </summary>
		/// <returns>Was the action successfully started</returns>
		bool Perform();

		void StopAction();
	}
}

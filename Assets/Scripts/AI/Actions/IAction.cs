using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public interface IAction : IReset
	{
		float Cost { get; }

		Vector2 StartingLocation { get; }

		bool InRange { get; set; }

		/// <summary>
		/// World sensors required by this action
		/// </summary>
		IEnumerable<WorldSensor> WorldSensors { get; }

		/// <summary>
		/// Is this action currently being performed
		/// </summary>
		bool IsRunning { get; }

		/// <summary>
		/// Has this action finished
		/// </summary>
		bool Completed { get; }

		/// <summary>
		/// Gets the effects of performing this action
		/// </summary>
		IEnumerable<WorldState> Effects { get; }

		bool PossibleGiven(WorldStates worldStates);

		/// <summary>
		/// Start doing this action
		/// </summary>
		/// <returns>Was the action successfully started</returns>
		bool Perform();

		void StopAction();
	}
}

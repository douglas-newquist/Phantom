using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public interface IEntity
	{
		GameObject gameObject { get; }
		Transform transform { get; }

		bool InCombat { get; }
		bool IsAlive { get; }

		/// <summary>
		/// Entities that are actively or have attacked this entity
		/// </summary>
		IEnumerable<IEntity> AttackedBy { get; }

		/// <summary>
		/// Gets the attitude of this entity towards another
		/// </summary>
		/// <param name="other">Target entity</param>
		Attitude GetAttitudeTowards(IEntity other);
	}
}

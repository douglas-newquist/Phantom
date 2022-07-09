using UnityEngine;

namespace Phantom
{
	public interface IEntity
	{
		GameObject gameObject { get; }
		Transform transform { get; }

		bool InCombat { get; }
		bool IsAlive { get; }
		Attitude GetAttitudeTowards(IEntity other);
	}
}

using UnityEngine;
using Phantom.Pathfinding;

namespace Phantom
{
	[System.Serializable]
	public class PathAgent<TMap, TCell>
	{
		public Pathfinder pathfinder;

		public PathAgent<TMap, TCell> agent;
	}
}

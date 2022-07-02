using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public abstract class RegionSelector : ScriptableObject
	{
		public const string CreateMenu = "Game/Generator/Region Selector/";

		public abstract IEnumerable<RectInt> GetRegions(RectInt totalArea);
	}
}

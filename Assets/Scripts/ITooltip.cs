using UnityEngine;

namespace Phantom
{
	public interface ITooltip
	{
		string DisplayName { get; }

		string Description { get; }

		Sprite Icon { get; }
	}
}

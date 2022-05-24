using System.Collections.Generic;

namespace Game
{
	public interface IRange<T>
	{
		T Min { get; set; }
		T Max { get; set; }
		T Delta { get; set; }
		T Center { get; set; }

		T Clamp(T value);
	}
}

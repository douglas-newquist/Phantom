using UnityEngine;

namespace Phantom
{
	public interface IGenerator<T>
	{
		/// <summary>
		/// Applies this generator to a specific area
		/// </summary>
		/// <param name="design"></param>
		/// <param name="area">Area to apply too</param>
		T Apply(T design, RectInt area);
		T Apply(T design);
		T ApplyOnce(T design, RectInt area);
		T Create(int width, int height);
	}
}

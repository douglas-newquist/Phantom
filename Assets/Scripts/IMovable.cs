using UnityEngine;

namespace Phantom
{
	public interface IMovable
	{
		void Move(Vector2 vector, Reference mode);

		void Stop();
	}
}

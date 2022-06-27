using UnityEngine;

namespace Phantom
{
	public interface IMover
	{
		void Move(Vector2 vector, Reference mode);

		void Brake();
	}
}

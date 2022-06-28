using UnityEngine;

namespace Phantom
{
	public interface IMover
	{
		Transform transform { get; }

		void Move(Vector2 vector, Reference mode);

		void MoveTo(Vector2 position);

		void Brake();
	}
}

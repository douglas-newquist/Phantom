using UnityEngine;
using System.Collections;

namespace Game
{
	public abstract class Controller : ScriptableObject
	{
		public abstract IEnumerator Control(Controllable controllable);
	}
}

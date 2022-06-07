using UnityEngine;
using System.Collections;

namespace Phantom
{
	public abstract class Controller : ScriptableObject
	{
		public abstract IEnumerator Control(Controllable controllable);
	}
}

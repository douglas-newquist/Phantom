using UnityEngine;
using Phantom.StatSystem;

namespace Phantom
{
	public class ItemSO : ScriptableObject
	{
		public virtual void Equip(StatSheet sheet) { }
	}
}

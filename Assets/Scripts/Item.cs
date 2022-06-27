using System.Collections.Generic;
using Phantom.StatSystem;

namespace Phantom
{
	[System.Serializable]
	public class Item
	{
		public ItemSO item;

		public Rarity rarity = Rarity.Common;

		public int level = 1;

		public List<StatValue> stats = new List<StatValue>();

		public void Equip(StatSheet sheet)
		{

		}
	}
}

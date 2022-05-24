using System.Collections.Generic;

namespace Game
{
	[System.Serializable]
	public class Item
	{
		public ItemSO item;

		public Rarity rarity = Rarity.Common;

		public int level = 1;

		public List<StatPair> stats = new List<StatPair>();

		public void Equip(StatSheet sheet)
		{

		}
	}
}

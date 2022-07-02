using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Game + "Level Object")]
	public class LevelObjectSO : MapTile
	{
		public GameObject prefab;
	}
}

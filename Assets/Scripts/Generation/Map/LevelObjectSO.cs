using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Game + "Level Object")]
	public class LevelObjectSO : MapTile
	{
		public GameObject prefab;

		[SerializeField]
		[Range(1, 16)]
		protected int width = 1, height = 1;

		public override int Width => width;

		public override int Height => height;
	}
}

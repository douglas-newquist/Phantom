using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Game + "Level Object")]
	public class LevelObjectSO : TileObjectSO
	{
		public GameObject prefab;

		[SerializeField]
		[Range(1, 16)]
		protected int width = 1, height = 1;

		public override int Width => width;

		public override int Height => height;

		public override GameObject Place(GameObject obj, TileObjectMap map, int x, int y)
		{
			var placed = Instantiate(prefab);
			placed.transform.SetParent(obj.transform);
			placed.transform.localPosition = new Vector3(x, y, 0);
			return placed;
		}
	}
}
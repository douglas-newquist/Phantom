using UnityEngine;

namespace Phantom
{
	public class ScaleVariation : MonoBehaviour, IVariation
	{
		[SerializeField]
		private FloatRange x = 1, y = 1, z = 1;

		private void Start() => Apply();

		private void OnSpawn() => Apply();

		public void Apply()
		{
			transform.localScale = new Vector3(x.Random, y.Random, z.Random);
		}
	}
}

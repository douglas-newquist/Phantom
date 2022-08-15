using UnityEngine;

namespace Phantom
{
	public class RotationVariation : MonoBehaviour, IVariation
	{
		[SerializeField]
		[MinMax(0, 360)]
		private FloatRange x, y, z = new FloatRange(0, 360);

		private void Start() => Apply();

		private void OnSpawn() => Apply();

		public void Apply()
		{
			transform.rotation = Quaternion.Euler(x.Random, y.Random, z.Random);
		}
	}
}

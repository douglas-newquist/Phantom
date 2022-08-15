using UnityEngine;

namespace Phantom
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class SpriteVariation : MonoBehaviour, IVariation
	{
		[SerializeField]
		private bool allowFlipX = true, allowFlipY = true;

		[SerializeField]
		private Sprite[] sprites;

		private void Start() => Apply();

		private void OnSpawn() => Apply();

		public void Apply()
		{
			if (sprites.Length == 0) return;
			var renderer = GetComponent<SpriteRenderer>();
			renderer.sprite = sprites[Random.Range(0, sprites.Length)];

			if (allowFlipX)
				renderer.flipX = Random.Range(0f, 1f) < 0.5f;
			if (allowFlipY)
				renderer.flipY = Random.Range(0f, 1f) < 0.5f;
		}
	}
}

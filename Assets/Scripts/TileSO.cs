using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[CreateAssetMenu()]
	public class TileSO : ScriptableObject
	{
		public Sprite[] sprites;

		public bool connectsToSelf = false;

		public TileConnectionSO[] up, down, left, right;
	}
}

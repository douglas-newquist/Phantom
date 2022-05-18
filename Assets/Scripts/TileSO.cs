using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TileSO : ScriptableObject
{
	public Sprite[] sprites;

	public bool connectsToSelf = false;

	public TileConnectionSO[] up, down, left, right;
}

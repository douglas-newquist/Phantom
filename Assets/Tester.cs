using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
	public class Tester : MonoBehaviour
	{
		public GameObject prefab;
		// Start is called before the first frame update
		void Start()
		{
			ObjectPool.Register("test", prefab);
			ObjectPool.Spawn("test");
		}

		// Update is called once per frame
		void Update()
		{
		}

		public void OnChanged(Event e)
		{
			Debug.Log(e);
			//Debug.Log(e.Context);
		}
	}
}

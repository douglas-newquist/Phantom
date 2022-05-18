using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class Tester : MonoBehaviour
	{
		public Stat stat;

		public ResourceStat resource;

		// Start is called before the first frame update
		void Start()
		{
			//resource = new ResourceStat();
			resource.Maximum.BaseValue = 10;
			resource.Current = 1;
			resource.Maximum.BaseValue = 100;
			resource.Percentage = 1;

			Debug.Log(resource.Maximum.Value);
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void OnChanged(Event e)
		{
			Debug.Log(e);
			Debug.Log(e.Context);
		}
	}
}

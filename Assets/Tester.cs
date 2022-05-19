using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
	public class Tester : MonoBehaviour
	{
		public Stat stat;

		public ResourceStat resource;
		// Start is called before the first frame update
		void Start()
		{
			resource.OnCurrentChanged.AddListener(OnChanged);
			resource.Maximum.AddModifier(new AdditiveModifier(this, 0, true, 10));
			resource.Percentage = 0.5f;
			resource.Maximum.AddModifier(new PercentageModifier(this, 20, true, 0.5f));
			resource.Maximum.AddModifier(new MultiplierModifier(this, 10, true, 2));
			Debug.Log(resource.Current + "/" + resource.Maximum.Value + " " + resource.Percentage);
			resource.Minimum.AddModifier(new AdditiveModifier(this, 0, true, 10));
			Debug.Log(resource.Current + "/" + resource.Maximum.Value + " " + resource.Percentage);
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

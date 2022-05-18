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
			stat.OnValueChanged.AddListener(OnChanged);
			stat.AddModifier(new AdditiveModifier(this, 0, true, 10));
			Debug.Log(stat.Value);
			stat.AddModifier(new PercentageModifier(this, 20, true, 0.5f));
			Debug.Log(stat.Value);
			stat.AddModifier(new MultiplierModifier(this, 10, true, 2));
			Debug.Log(stat.Value);
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

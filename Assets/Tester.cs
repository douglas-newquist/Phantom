using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
	public class Tester : MonoBehaviour
	{
		public Damage damage;
		public ResourceStatSO hull, armor, shield;

		// Start is called before the first frame update
		void Start()
		{
			var sheet = gameObject.AddComponent<StatSheet>();
			sheet.GetStat(hull).BaseValue = 100;
			sheet.GetStat(armor).BaseValue = 100;
			sheet.GetStat(shield).BaseValue = 100;


			Debug.Log(sheet);

			damage.Apply(sheet);

			Debug.Log(sheet);
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

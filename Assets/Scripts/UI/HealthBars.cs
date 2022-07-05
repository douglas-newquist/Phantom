using UnityEngine;
using UnityEngine.UI;
using Phantom.StatSystem;

namespace Phantom.UI
{
	public class HealthBars : MonoBehaviour
	{
		public HealthBar[] Bars => GetComponentsInChildren<HealthBar>();

		public void SetStatSheet(StatSheet statSheet)
		{
			foreach (var bar in Bars)
				bar.Attach(statSheet);
		}

		public void SetStatSheet(GameObject gameObject)
		{
			SetStatSheet(gameObject.GetComponent<StatSheet>());
		}
	}
}

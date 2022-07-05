using UnityEngine;
using UnityEngine.UI;
using Phantom.StatSystem;

namespace Phantom.UI
{
	[System.Serializable]
	[RequireComponent(typeof(Slider))]
	[ExecuteInEditMode]
	public class HealthBar : MonoBehaviour
	{
		[SerializeField]
		private ResourceStatType resource;

		[SerializeField]
		private Slider slider;

		private IResourceStat stat = null;

		[SerializeField]
		private StatSheet statSheet;

		private void Start()
		{
			if (statSheet != null)
				Attach(statSheet);
		}

		private void OnCurrentChanged(float current)
		{
			if (stat != null || statSheet.TryGetStat(resource, out stat))
				slider.value = stat.Percentage;
		}

		public void Detach()
		{
			if (stat != null)
				stat.OnCurrentChanged.RemoveListener(OnCurrentChanged);

			stat = null;
			statSheet = null;
		}

		public void Attach(StatSheet statSheet)
		{
			if (this.statSheet != null || stat != null)
				Detach();

			this.statSheet = statSheet;
			if (statSheet.TryGetStat(resource, out stat))
			{
				stat.OnCurrentChanged.AddListener(OnCurrentChanged);
				OnCurrentChanged(stat.Current);
			}
		}

		private void OnGUI()
		{
			if (slider == null)
				slider = GetComponent<Slider>();
		}
	}
}

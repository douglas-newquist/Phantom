using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class TurretController : MonoBehaviour
	{
		private List<List<ITurret>> groups = new List<List<ITurret>>();

		private List<ITurret> GetGroup(int group)
		{
			if (group > 0 && group < groups.Count)
				return groups[group];
			return null;
		}

		public void Fire(Vector3 vector, int group, Reference mode)
		{
			var turrets = GetGroup(group);

			if (turrets != null)
				foreach (var turret in turrets)
					turret.Fire(vector, mode);
		}

		public void Look(Vector3 vector, int group, Reference mode)
		{
			var turrets = GetGroup(group);

			if (turrets != null)
				foreach (var turret in turrets)
					turret.Look(vector, mode);
		}
	}
}

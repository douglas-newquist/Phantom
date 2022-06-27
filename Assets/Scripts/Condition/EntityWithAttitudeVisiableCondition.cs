using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Condition + "Entity with Attitude Visiable")]
	public class EntityWithAttitudeVisiableCondition : Condition
	{
		public Attitude attitudes = Attitude.Hostile;

		[MinMax(0, Level.TotalSizeLimit)]
		public FloatRange range = new FloatRange(0, Level.TotalSizeLimit / 8);

		public override bool Satisfied(GameObject gameObject)
		{
			throw new System.NotImplementedException();
		}
	}
}

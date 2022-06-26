using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Condition + "Composite")]
	public class CompositeCondition : Condition
	{
		public enum Mode { All, Any }

		public Mode where = Mode.All;

		public Condition[] conditions;

		public override bool Satisfied(GameObject gameObject)
		{
			foreach (var condition in conditions)
			{
				if (condition != null)
				{
					switch (where)
					{
						case Mode.All:
							if (!condition.Satisfied(gameObject))
								return false;
							break;

						case Mode.Any:
							if (condition.Satisfied(gameObject))
								return true;
							break;
					}
				}
			}

			switch (where)
			{
				case Mode.Any:
					return false;

				default:
				case Mode.All:
					return true;
			}
		}
	}
}

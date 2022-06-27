using Phantom.StatSystem;

namespace Phantom
{
	public class Gyro : ShipComponent
	{
		public StatType torqueStat;

		public float force = 1;

		public float Spin()
		{
			return force;
		}
	}
}

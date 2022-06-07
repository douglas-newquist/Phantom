namespace Phantom
{
	public class Gyro : ShipComponent
	{
		public StatSO torqueStat;

		public float force = 1;

		public float Spin()
		{
			return force;
		}
	}
}

namespace Phantom
{
	public static class AttitudeExtensions
	{
		/// <summary>
		/// Checks if the given attitude is one of the given attitudes
		/// </summary>
		/// <param name="attitude">This attitude</param>
		/// <param name="attitudes">Attitudes to match</param>
		public static bool Matches(this Attitude attitude, Attitude attitudes)
		{
			switch (attitudes)
			{
				case Attitude.AnyAttitude:
					return true;

				default:
					return attitudes.HasFlag(attitude);
			}
		}
	}
}

namespace Game
{
	public interface IResourceStat
	{
		IStat Maximum { get; }

		IStat Minimum { get; }

		/// <summary>
		/// Gets/Sets the current amount of this resource
		/// </summary>
		float Current { get; set; }

		float Percentage { get; set; }

		/// <summary>
		///
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="allOrNothing">If true only withdraws if the full amount can be withdrawn</param>
		/// <returns></returns>
		float Withdraw(float amount, bool allOrNothing = false);

		/// <summary>
		/// Adds an amount of resource to the current supply
		/// </summary>
		/// <param name="amount">The amount of the resource to deposit</param>
		/// <param name="allOrNothing">If true only deposits if the full amount can be deposited</param>
		/// <returns>The amount actually deposited</returns>
		float Deposit(float amount, bool allOrNothing = false);
	}
}

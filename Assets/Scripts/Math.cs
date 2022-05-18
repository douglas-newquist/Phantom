public static class Math
{
	public static float ToPercentage(float value, float min, float max)
	{
		float divisor = max - min;
		value -= min;

		if (divisor == 0)
			return value == 0 ? 0 : 1;

		return value / divisor;
	}

	public static float FromPercentage(float value, float min, float max)
	{
		return value * (max - min) + min;
	}
}

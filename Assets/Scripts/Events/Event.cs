namespace Game
{
	public class Event
	{
		public object Context { get; protected set; }

		public Event(object context)
		{
			Context = context;
		}

		public override string ToString()
		{
			return "Generic Event";
		}
	}

}

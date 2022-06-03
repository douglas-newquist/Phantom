using UnityEngine;

namespace Game
{
	public struct BroadcastSpawner : ISpawner
	{
		public string[] methods;

		public SendMessageOptions options;

		public BroadcastSpawner(SendMessageOptions options, params string[] methods)
		{
			this.methods = methods;
			this.options = options;
		}

		public BroadcastSpawner(params string[] methods)
		{
			this.methods = methods;
			this.options = SendMessageOptions.DontRequireReceiver;
		}

		public void Spawn(GameObject obj)
		{
			if (methods != null)
				foreach (var method in methods)
					obj.BroadcastMessage(method, options);
		}
	}
}

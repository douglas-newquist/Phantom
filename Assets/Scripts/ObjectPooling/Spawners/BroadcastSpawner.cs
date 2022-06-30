using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Calls the given method on the newly spawned object
	/// </summary>
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

		public bool Spawn(GameObject obj)
		{
			if (methods != null)
				foreach (var method in methods)
					obj.BroadcastMessage(method, options);

			return true;
		}
	}
}

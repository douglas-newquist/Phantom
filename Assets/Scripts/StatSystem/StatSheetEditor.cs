using UnityEditor;

#if UNITY_EDITOR

namespace Phantom.StatSystem
{
	[CustomEditor(typeof(StatSheet))]
	public class StatSheetEditor : Editor
	{
		public static bool showStats = false;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var sheet = target as StatSheet;

			if (EditorGUILayout.Toggle("Reset", false))
				foreach (var stat in sheet.GetStatsOfType<IReset>())
					stat.Reset();

			showStats = EditorGUILayout.Foldout(showStats, "Stats " + sheet.StatCount);
			if (showStats)
			{
				if (sheet.StatCount == 0)
					EditorGUILayout.LabelField("No stats.");

				foreach (var stat in sheet)
					EditorGUILayout.LabelField(stat.ToString());
			}
		}
	}
}

#endif

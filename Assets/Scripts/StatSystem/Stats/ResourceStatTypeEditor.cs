using UnityEditor;

#if UNITY_EDITOR

namespace Phantom.StatSystem
{
	[CustomEditor(typeof(ResourceStatType))]
	public class ResourceStatTypeEditor : StatTypeEditor
	{
		SerializedProperty startingPercentage => serializedObject.FindProperty("startingPercentage");
		SerializedProperty maxChangedMode => serializedObject.FindProperty("maxChangedMode");


		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			EditorGUILayout.PropertyField(startingPercentage);
			EditorGUILayout.PropertyField(maxChangedMode);
		}
	}
}

#endif

using UnityEditor;

namespace Phantom.StatSystem.Editors
{
#if UNITY_EDITOR
	[CustomEditor(typeof(ResourceStatType))]
	public class ResourceStatTypeEditor : StatTypeEditor
	{
		SerializedProperty startingPercentage => serializedObject.FindProperty("startingPercentage");
		SerializedProperty maxChangedMode => serializedObject.FindProperty("maxChangedMode");


		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			serializedObject.Update();
			EditorGUILayout.PropertyField(startingPercentage);
			EditorGUILayout.PropertyField(maxChangedMode);
			serializedObject.ApplyModifiedProperties();
		}
	}
#endif
}

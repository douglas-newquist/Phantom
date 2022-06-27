using UnityEditor;

#if UNITY_EDITOR

namespace Phantom.StatSystem
{
	[CustomEditor(typeof(StatType))]
	public class StatTypeEditor : Editor
	{
		SerializedProperty displayName;
		SerializedProperty description;
		SerializedProperty icon;
		SerializedProperty defaultValue;
		SerializedProperty limits;
		SerializedProperty canBeModified;

		protected virtual void OnEnable()
		{
			displayName = serializedObject.FindProperty("displayName");
			description = serializedObject.FindProperty("description");
			icon = serializedObject.FindProperty("icon");
			defaultValue = serializedObject.FindProperty("defaultValue");
			limits = serializedObject.FindProperty("limits");
			canBeModified = serializedObject.FindProperty("canBeModified");
		}

		public override void OnInspectorGUI()
		{
			EditorGUILayout.PropertyField(displayName);
			EditorGUILayout.PropertyField(description);
			EditorGUILayout.PropertyField(icon);

			FloatRange range = (target as StatType).Limits;
			EditorGUILayout.Slider(defaultValue, range.Min, range.Max);

			EditorGUILayout.PropertyField(limits);
			EditorGUILayout.PropertyField(canBeModified);
		}
	}
}

#endif

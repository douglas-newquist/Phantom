using UnityEditor;


namespace Phantom.StatSystem.Editors
{
#if UNITY_EDITOR
	[CustomEditor(typeof(StatType))]
	public class StatTypeEditor : Editor
	{
		SerializedProperty displayName;
		SerializedProperty description;
		SerializedProperty icon;
		SerializedProperty defaultValue;
		SerializedProperty limits;
		SerializedProperty canBeModified;
		SerializedProperty statusEffects;

		protected virtual void OnEnable()
		{
			displayName = serializedObject.FindProperty("displayName");
			description = serializedObject.FindProperty("description");
			icon = serializedObject.FindProperty("icon");
			defaultValue = serializedObject.FindProperty("defaultValue");
			limits = serializedObject.FindProperty("limits");
			canBeModified = serializedObject.FindProperty("canBeModified");
			statusEffects = serializedObject.FindProperty("statusEffects");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(displayName);
			EditorGUILayout.PropertyField(description);
			EditorGUILayout.PropertyField(icon);

			FloatRange range = (target as StatType).Limits;
			EditorGUILayout.Slider(defaultValue, range.Min, range.Max);

			EditorGUILayout.PropertyField(limits);
			EditorGUILayout.PropertyField(canBeModified);
			EditorGUILayout.PropertyField(statusEffects);
			serializedObject.ApplyModifiedProperties();
		}
	}
#endif
}

using UnityEngine;
using UnityEditor;

namespace Phantom.StatSystem.Editors
{
#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(StatValue))]
	public class StatValueDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			if (!label.text.StartsWith("Element "))
				position = EditorGUI.PrefixLabel(position, label);

			var statRect = new Rect(position.x, position.y, position.width / 2 - 5, position.height);
			var valueRect = new Rect(statRect.xMax + 5, position.y, statRect.width, position.height);

			var type = property.FindPropertyRelative("type");
			var value = property.FindPropertyRelative("value");

			EditorGUI.PropertyField(statRect, type, GUIContent.none);

			if (type.objectReferenceValue != null)
			{
				StatType s = (StatType)type.objectReferenceValue;
				if (value.floatValue == 0)
					value.floatValue = s.DefaultValue;
				EditorGUI.Slider(valueRect, value, -s.Limits.Max, s.Limits.Max, GUIContent.none);
			}
			else
				EditorGUI.LabelField(valueRect, "Select stat");

			EditorGUI.EndProperty();
		}
	}
#endif
}

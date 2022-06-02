using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace Game
{
	[CustomPropertyDrawer(typeof(IntRange))]
	[CustomPropertyDrawer(typeof(FloatRange))]
	public class RangePropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, label);

			var minRect = new Rect(position.x, position.y, position.width / 2 - 5, position.height);
			var maxRect = new Rect(minRect.xMax + 5, position.y, minRect.width, position.height);

			var min = property.FindPropertyRelative("min");
			var max = property.FindPropertyRelative("max");

			EditorGUI.PropertyField(minRect, min, GUIContent.none);
			EditorGUI.PropertyField(maxRect, max, GUIContent.none);

			EditorGUI.EndProperty();
		}
	}
}
#endif

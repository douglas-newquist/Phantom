using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace Phantom
{
	[CustomPropertyDrawer(typeof(StatPair))]
	public class StatPairDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, label);

			var statRect = new Rect(position.x, position.y, position.width / 2 - 5, position.height);
			var valueRect = new Rect(statRect.xMax + 5, position.y, statRect.width, position.height);

			var stat = property.FindPropertyRelative("stat");
			var value = property.FindPropertyRelative("baseValue");

			EditorGUI.PropertyField(statRect, stat, GUIContent.none);

			if (stat.objectReferenceValue != null)
			{
				StatSO s = (StatSO)stat.objectReferenceValue;
				EditorGUI.Slider(valueRect, value, s.limits.Min, s.limits.Max, GUIContent.none);
			}
			else
				EditorGUI.LabelField(valueRect, "Select stat");

			EditorGUI.EndProperty();
		}
	}
}
#endif

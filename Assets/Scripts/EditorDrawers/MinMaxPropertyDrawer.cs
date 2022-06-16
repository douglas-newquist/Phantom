using UnityEngine;
using UnityEditor;

namespace Phantom
{
	[CustomPropertyDrawer(typeof(Sorted))]
	public class SortedPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);


			var sorted = (Sorted)attribute;


			EditorGUI.EndProperty();
		}
	}
}
#if UNITY_EDITOR
namespace Phantom
{
	[CustomPropertyDrawer(typeof(MinMax))]
	public class MinMaxPropertyDrawer : PropertyDrawer
	{
		const float Spacing = 5;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, label);

			var minRect = new Rect(position.x, position.y, 45, position.height);
			var maxRect = new Rect(position.xMax - 45, position.y, 45, position.height);
			var sliderRect = new Rect(minRect.xMax + Spacing,
							 position.y,
							 position.width - (minRect.width + Spacing) * 2,
							 position.height);

			var min = property.FindPropertyRelative("min");
			var max = property.FindPropertyRelative("max");

			EditorGUI.PropertyField(minRect, min, GUIContent.none);
			EditorGUI.PropertyField(maxRect, max, GUIContent.none);

			var range = (MinMax)attribute;

			switch (min.propertyType)
			{
				case SerializedPropertyType.Integer:
					IntSlider(sliderRect, min, max, range);
					break;

				case SerializedPropertyType.Float:
					FloatSlider(sliderRect, min, max, range);
					break;

				default:
					EditorGUI.LabelField(sliderRect, "MinMax not implemented for " + min.propertyType);
					break;
			}


			EditorGUI.EndProperty();
		}

		private void IntSlider(Rect rect, SerializedProperty min, SerializedProperty max, MinMax range)
		{
			float low = min.intValue, high = max.intValue;
			EditorGUI.MinMaxSlider(rect, ref low, ref high, range.min, range.max);
			min.intValue = Mathf.RoundToInt(Mathf.Min(low, high));
			max.intValue = Mathf.RoundToInt(Mathf.Max(low, high));
		}

		private void FloatSlider(Rect rect, SerializedProperty min, SerializedProperty max, MinMax range)
		{
			float low = min.floatValue, high = max.floatValue;
			EditorGUI.MinMaxSlider(rect, ref low, ref high, range.min, range.max);
			min.floatValue = Mathf.Min(low, high);
			max.floatValue = Mathf.Max(low, high);
		}
	}
}
#endif

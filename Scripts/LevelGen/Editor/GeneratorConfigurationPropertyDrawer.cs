using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace RougeLevelGen.Editor
{
	[CustomPropertyDrawer(typeof(GeneratorConfiguration))]
	public class GeneratorConfigurationPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			var layer = property.FindPropertyRelative("layer");
			var type = property.FindPropertyRelative("type");
			// EditorGUILayout.PropertyField(, true);
			// 
			// EditorGUILayout.PropertyField(property.FindPropertyRelative("maxWalkers"), true);
			// EditorGUILayout.PropertyField(property.FindPropertyRelative("repeat"), true);
			Rect r1 = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
			float y = r1.y + EditorGUIUtility.singleLineHeight;
			Rect r2 = new Rect(position.x, y, r1.width, EditorGUIUtility.singleLineHeight);
			y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(r2, layer);
			EditorGUI.PropertyField(r1, type);

			var v = type.GetEnumValue<GeneratorTypes>();
			if (v == GeneratorTypes.DrunkWalk)
			{
				var maxWalks = property.FindPropertyRelative("maxWalkers");
				Rect r3 = new Rect(position.x ,y,position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r3, maxWalks,new GUIContent("Maximum Walks"));
				// y += EditorGUIUtility.singleLineHeight;
			}else if (v == GeneratorTypes.Smooth)
			{
				var repeat = property.FindPropertyRelative("repeat");
				Rect r3 = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r3, repeat, new GUIContent("Additional Times Smoothing"));
				// y +=EditorGUIUtility.singleLineHeight;
			}

			// Draw label
			//position = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
			//position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var type = property.FindPropertyRelative("type");
			var v = type.GetEnumValue<GeneratorTypes>();
			
			int extraFields = 1;
			
			
			return EditorGUIUtility.singleLineHeight * (extraFields+2);
			// return base.GetPropertyHeight(property, label);
		}
	}
}
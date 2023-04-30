using UnityEditor;
using UnityEngine;
using HDyar.RougeLevelGen;

namespace HDyar.RougeLevelGen.Editor
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

			
			var v = (GeneratorTypes)type.enumValueIndex;
			if (v == GeneratorTypes.DrunkWalk)
			{
				var desired = property.FindPropertyRelative("desiredPercentageFloorFill");
				Rect r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, desired, new GUIContent("Percentage Floor Fill"));
				y += EditorGUIUtility.singleLineHeight;
				
				var maxWalks = property.FindPropertyRelative("maxWalkers");
				r = new Rect(position.x ,y,position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, maxWalks,new GUIContent("Maximum Walkers"));
				y += EditorGUIUtility.singleLineHeight;
				
				var chanceToSpawnNewWalker = property.FindPropertyRelative("chanceToSpawnNewWalker");
				r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, chanceToSpawnNewWalker);
				y += EditorGUIUtility.singleLineHeight;
				
				var chanceToDestroyWalker = property.FindPropertyRelative("chanceToDestroyWalker");
				r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, chanceToDestroyWalker);
			}else if (v == GeneratorTypes.Smooth)
			{
				var repeat = property.FindPropertyRelative("repeat");
				Rect r3 = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r3, repeat, new GUIContent("Additional Times Smoothing"));
			}else if (v == GeneratorTypes.Noise)
			{
				var desired = property.FindPropertyRelative("desiredPercentageFloorFill");
				Rect r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, desired, new GUIContent("Percentage Floor Fill"));
			}
			else if (v == GeneratorTypes.SimpleCellularAutomata)
			{
				var repeat = property.FindPropertyRelative("repeat");
				Rect r3 = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r3, repeat, new GUIContent("Repeat"));
			}
			else if (v == GeneratorTypes.Perlin)
			{
				var desired = property.FindPropertyRelative("desiredPercentageFloorFill");
				Rect r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, desired, new GUIContent("Floor to Wall Ratio"));
				y += EditorGUIUtility.singleLineHeight;
				
				var scale = property.FindPropertyRelative("scale");
				r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, scale, new GUIContent("Noise Scale"));
			}else if (v == GeneratorTypes.LevelEdges)
			{
				var thick = property.FindPropertyRelative("thickness");
				Rect r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, thick, new GUIContent("Thickness"));
			}else if (v == GeneratorTypes.Merge)
			{
				var otherLayer = property.FindPropertyRelative("otherLayer");
				Rect r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, otherLayer, new GUIContent("Other Layer"));
				y += EditorGUIUtility.singleLineHeight;

				var merge = property.FindPropertyRelative("mergeOperation");
				r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, merge, new GUIContent("Operation"));
			}else if (v == GeneratorTypes.Fill)
			{
				var tile = property.FindPropertyRelative("tile");
				Rect r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, tile, new GUIContent("Fill With"));
			}else if (v == GeneratorTypes.FillIslands)
			{
				var tile = property.FindPropertyRelative("tile");
				Rect r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, tile, new GUIContent("Remove all but largest Islands of"));
			}else if (v == GeneratorTypes.PoissonDisk)
			{
				var scale = property.FindPropertyRelative("scale");
				Rect r = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
				EditorGUI.PropertyField(r, scale, new GUIContent("Test Radius"));
			}

			// Draw label
			//position = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
			//position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var type = property.FindPropertyRelative("type");
			var v = (GeneratorTypes)type.enumValueIndex;
			
			int extraFields = 1;
			if (v == GeneratorTypes.DrunkWalk)
			{
				extraFields = 4;
			}else if (v == GeneratorTypes.Invert)
			{
				extraFields = 0;
			}else if (v == GeneratorTypes.Perlin || v == GeneratorTypes.Merge)
			{
				extraFields = 2;
			}
			
			return EditorGUIUtility.singleLineHeight * (extraFields+2);
			// return base.GetPropertyHeight(property, label);
		}
	}
}
using System;
using RougeLevelGen;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
	private SerializedProperty genOnStart;
	private SerializedProperty genSettings;
	private SerializedProperty gens;
	private SerializedProperty builders;

	private void OnEnable()
	{
		genOnStart = serializedObject.FindProperty("generateOnStart");
		genSettings = serializedObject.FindProperty("_settings");
		gens = serializedObject.FindProperty("_generationLayersSetup");
		builders = serializedObject.FindProperty("_builders");
	}

	public override void OnInspectorGUI()
	{
		var lg = target as LevelGenerator;
		serializedObject.Update();
		EditorGUILayout.PropertyField(genOnStart);
		EditorGUILayout.PropertyField(genSettings,new GUIContent("Global Generation Settings"));
		EditorGUILayout.Separator();
		EditorGUILayout.PropertyField(gens, new GUIContent("Generators"));
		EditorGUILayout.PropertyField(builders, new GUIContent("Builders"));
		EditorGUILayout.Separator();
		
		if (GUILayout.Button(new GUIContent("Generate")))
		{ 
			lg.Generate();
		}

		if (GUILayout.Button(new GUIContent("Cancel")))
		{
			lg.Cancel();
		}
		
		EditorGUILayout.LabelField(LevelGenerator.ProgressStage);

		serializedObject.ApplyModifiedProperties();
	}

	// Custom GUILayout progress bar.
	void ProgressBar(float value, string label)
	{
		// Get a rect for the progress bar using the same margins as a textfield:
		Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
		EditorGUI.ProgressBar(rect, value, label);
		EditorGUILayout.Space();
	}
}

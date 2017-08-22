using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NE_PopUpWindow : EditorWindow {

	#region Variables
	static NE_PopUpWindow currentPopUp;
	string graphName = "Enter a name...";
	#endregion

	#region Constructor
	#endregion

	#region Main Methods
	public static void InitNodePopUp() {
		currentPopUp = (NE_PopUpWindow)EditorWindow.GetWindow<NE_PopUpWindow>();
		currentPopUp.titleContent = new GUIContent("Node PopUp");
	}

	void OnGUI () {
		GUILayout.Space (20);
		GUILayout.BeginHorizontal();
		GUILayout.Space (20);

		GUILayout.BeginVertical ();

		EditorGUILayout.LabelField("Create New Graph:", EditorStyles.boldLabel);
		graphName = EditorGUILayout.TextField("Graph Name:", graphName);

		GUILayout.Space(10);

		GUILayout.BeginHorizontal();

		if (GUILayout.Button("Create Graph", GUILayout.Height(40))) {
			if (!string.IsNullOrEmpty(graphName) && graphName != "Enter a name...") {
				NE_NodeUtils.CreateNewGraph(graphName);
				currentPopUp.Close();
			} else {
				EditorUtility.DisplayDialog("Error", "Please enter a graph name", "OK");
			}

		}

		if (GUILayout.Button("Cancel", GUILayout.Height(40))) {
			currentPopUp.Close();
		}

		GUILayout.EndHorizontal();

		GUILayout.EndVertical ();

		GUILayout.Space (20);
		GUILayout.EndHorizontal();
		GUILayout.Space (20);
	}
	#endregion
}

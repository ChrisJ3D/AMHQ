using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class NE_NodeUtils {
	
		public static void CreateNewGraph(string name) {
			Debug.Log("NODEUTILS: Creating new graph called " + name);
			NE_NodeGraph currentGraph = (NE_NodeGraph)ScriptableObject.CreateInstance<NE_NodeGraph>();

			if (currentGraph != null) {
				currentGraph.graphName = name;
				currentGraph.InitGraph();

				AssetDatabase.CreateAsset(currentGraph, "Assets/Scripts/Editor/NodeEditor/Database/" + name + ".asset");
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();

				NE_Window currentWindow = (NE_Window)EditorWindow.GetWindow<NE_Window>();
				if (currentWindow != null) {
					currentWindow.nodeGraph = currentGraph;
				}

			} else {
				EditorUtility.DisplayDialog("Error", "Unable to create graph", "OK");
			}

		}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class NE_NodeUtils {
	
	public static void CreateNewGraph(string name) {
		Debug.Log("NODEUTILS: Creating new graph called " + name);
		NE_NodeGraph currentGraph = (NE_NodeGraph)ScriptableObject.CreateInstance<NE_NodeGraph>();

		if (currentGraph) {
			currentGraph.graphName = name;
			currentGraph.InitGraph();

			AssetDatabase.CreateAsset(currentGraph, "Assets/Scripts/Editor/NodeEditor/Database/" + name + ".asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			NE_Window currentWindow = (NE_Window)EditorWindow.GetWindow<NE_Window>();
			if (currentWindow) {
				currentWindow.nodeGraph = currentGraph;
			}

		} else {
			EditorUtility.DisplayDialog("Error", "Unable to create graph", "OK");
		}
	}

	public static void LoadGraph() {
		NE_NodeGraph currentGraph = null;

		string graphPath = EditorUtility.OpenFilePanel("Load Graph", Application.dataPath + "/Scripts/Editor/NodeEditor/Database", "");
		int dataPathLength = Application.dataPath.Length;
		string finalPath = graphPath.Substring(dataPathLength - 6);

		currentGraph = (NE_NodeGraph)AssetDatabase.LoadAssetAtPath(finalPath, typeof(NE_NodeGraph));

		if (currentGraph) {
			NE_Window currentWindow = (NE_Window)EditorWindow.GetWindow<NE_Window>();
			if (currentWindow) {
				currentWindow.nodeGraph = currentGraph;
			}

		} else {
			EditorUtility.DisplayDialog("Error", "Unable to load selected graph", "OK");
		}

	}

	public static void UnloadGraph() {
		NE_Window currentWindow = (NE_Window)EditorWindow.GetWindow<NE_Window>();
		if (currentWindow) {
			currentWindow.nodeGraph = null;
		}
	}

	public static void CreateNode(NE_NodeGraph currentGraph, NodeType nodeType, Vector2 mousePosition) {

		if(currentGraph) {
			NE_NodeBase currentNode = null;
			
			switch(nodeType) {
				case NodeType.Float:
				currentNode = (NE_FloatNode)ScriptableObject.CreateInstance<NE_FloatNode>();
				currentNode.nodeName = "Float";
				break;

				case NodeType.Add:
				currentNode = (NE_AddNode)ScriptableObject.CreateInstance<NE_AddNode>();
				currentNode.nodeName = "Add";
				break;

				default:
					break;
			}

			if (currentNode) {
				currentNode.InitNode();
				currentNode.nodeRect.x = mousePosition.x;
				currentNode.nodeRect.y = mousePosition.y;
				currentNode.parentGraph = currentGraph;
				
				currentGraph.nodes.Add(currentNode);

				AssetDatabase.AddObjectToAsset(currentNode,currentGraph);
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
		}
	}

	public static void DeleteNode(int index, NE_NodeGraph currentGraph) {
		if (currentGraph) {
			
			if (currentGraph.nodes.Count >= index) {
				NE_NodeBase nodeToDelete = currentGraph.nodes[index];

				if (nodeToDelete) {
					currentGraph.nodes.RemoveAt(index);
					GameObject.DestroyImmediate(nodeToDelete, true);
					AssetDatabase.SaveAssets();
					AssetDatabase.Refresh();
				}
			}
		}
	}
}

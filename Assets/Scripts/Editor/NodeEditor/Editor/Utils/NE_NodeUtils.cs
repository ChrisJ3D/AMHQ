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
			SaveGraph();

			NE_Window currentWindow = (NE_Window)EditorWindow.GetWindow<NE_Window>();
			if (currentWindow) {
				currentWindow.nodeGraph = currentGraph;
			}

		} else {
			EditorUtility.DisplayDialog("Error", "Unable to create graph", "OK");
		}
	}

	public static void SaveGraph() {
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
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
				currentNode.position.x = mousePosition.x;
				currentNode.position.y = mousePosition.y;
				currentNode.parentGraph = currentGraph;
				
				currentNode.InitNode();
				currentGraph.nodes.Add(currentNode);

				AssetDatabase.AddObjectToAsset(currentNode,currentGraph);
				SaveGraph();
			}
		}
	}

	public static void DuplicateNode(int index, NE_NodeGraph currentGraph) {
		if (currentGraph) {
			
			if (currentGraph.nodes.Count >= index) {

				NE_NodeBase currentNode = null;

				NE_NodeBase nodeToDuplicate = currentGraph.nodes[index];

				currentNode = (NE_NodeBase)ScriptableObject.CreateInstance(nodeToDuplicate.GetType().ToString());

				if (currentNode) {
					currentNode.position = nodeToDuplicate.position;
					currentNode.nodeRect = nodeToDuplicate.nodeRect;
					currentNode.nodeType = nodeToDuplicate.nodeType;
					currentNode.nodeValue = nodeToDuplicate.nodeValue;
					currentNode.parentGraph = currentGraph;
					currentNode.nodeName = nodeToDuplicate.nodeName;

					//currentNode.InitNode();
					
					currentGraph.nodes.Add(currentNode);

					AssetDatabase.AddObjectToAsset(currentNode,currentGraph);
					SaveGraph();
				}
			}
		}
	}

	public static void DeleteNode(int index, NE_NodeGraph currentGraph) {
		if (currentGraph) {
			
			if (currentGraph.nodes.Count >= index) {
				NE_NodeBase nodeToDelete = currentGraph.nodes[index];

				if (nodeToDelete) {
					currentGraph.nodes.RemoveAt(index);
					currentGraph.showProperties = false;

					foreach (NE_NodeConnectorBase connection in nodeToDelete.connectors) {
						connection.inputConnector = null;
					}

					GameObject.DestroyImmediate(nodeToDelete, true);
					SaveGraph();
				}
			}
		}
	}

	public static void DrawLineToMouse(NE_NodeConnectorBase connector, Vector2 mousePosition, string orientation) {
		Vector3 origin = connector.GetConnectionLinePosition();
		Vector3 destination = new Vector3(mousePosition.x, mousePosition.y);

		DrawLine(origin, destination, orientation);
	}

	public static void DrawLine(Vector3 origin, Vector3 destination, string orientation) {
		
		Vector3 startTangent = new Vector3();
		Vector3 endTangent = new Vector3();

		switch(orientation) {
			case "Right":
				startTangent = origin + Vector3.left * 50;
				endTangent = destination + Vector3.right * 50;
				break;

			case "Left":
				startTangent = origin + Vector3.right * 50;
				endTangent = destination + Vector3.left * 50;
				break;

			case "Up":
				startTangent = origin + Vector3.up * 50;
				endTangent = destination + Vector3.down * 50;
				break;

			case "Down":
				startTangent = origin + Vector3.down * 50;
				endTangent = destination + Vector3.up * 50;
				break;

			default:
				startTangent = origin + Vector3.right * 50;
				endTangent = destination + Vector3.left * 50;
				break;
		}
		
		Handles.BeginGUI();
			Handles.color = Color.white;
			Handles.DrawBezier(origin, destination, startTangent, endTangent, Color.gray, null, 3f);
		Handles.EndGUI();
	}
}

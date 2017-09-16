using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class NE_NodeInput : ScriptableObject {

	public int index;
	public bool isOccupied = false;
	public bool allowsMultipleInputs = false;
	public NE_NodeBase parentNode;

	public Vector3 GetConnectionPosition() {
		float x = parentNode.nodeRect.x + parentNode.nodeRect.width + 10f;
		float y = ((parentNode.nodeRect.y + parentNode.nodeRect.height) / (parentNode.numberOfInputs + 1)) * index;
		
		return new Vector3(x,y);
	}

	public void DrawConnector() {
		Rect nodeRect = parentNode.nodeRect;
		NE_NodeGraph parentGraph = parentNode.parentGraph;
	}

	public void DrawLine(Vector3 origin, Vector3 destination) {

		Vector3 startTangent = origin + Vector3.right * 50;
		Vector3 endTangent = destination + Vector3.left * 50;
		
		Handles.BeginGUI();
			Handles.color = Color.white;
			Handles.DrawBezier(origin, destination, startTangent, endTangent, Color.gray, null, 3f);
		Handles.EndGUI();
    }
}

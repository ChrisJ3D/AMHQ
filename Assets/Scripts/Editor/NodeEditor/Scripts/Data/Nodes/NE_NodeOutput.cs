using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class NE_NodeOutput : ScriptableObject {

	public int index;
	public bool isOccupied = false;
	public NE_NodeBase parentNode;

	public Vector3 GetConnectionPosition() {
		float x = parentNode.nodeRect.x + parentNode.nodeRect.width + 10f;
		float y = ((parentNode.nodeRect.y + parentNode.nodeRect.height) / (parentNode.numberOfInputs + 1)) * index;
		
		return new Vector3(x,y);
	}
}

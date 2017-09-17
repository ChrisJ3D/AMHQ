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
	public Vector2 size = new Vector2(24f, 24f);
	public Vector2 position = new Vector2(0.0f, 0.0f);
	
	public void GetConnectionPosition() {
		if (parentNode) {
			float top = parentNode.nodeRect.y + (size.y * 0.5f);
			float center = top + (parentNode.nodeRect.height * 0.5f) - size.y;
			
			float fraction = (index + 1) / ((float)parentNode.numberOfInputs + 1);
			float fractionOffset = (fraction - 0.5f) * 2f;
			
			position.y = center + (fractionOffset * (parentNode.nodeRect.height /2));

			position.x = parentNode.nodeRect.x - size.x;
		}
	}
}

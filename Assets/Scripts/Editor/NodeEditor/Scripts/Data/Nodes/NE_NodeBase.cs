using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_NodeBase : ScriptableObject {

	//	PUBLIC VARIABLES
	public string nodeName;
	public Rect nodeRect;
	public NE_NodeGraph parentGraph;
	public NodeType nodeType;
	public bool isSelected = false;

	//	PRIVATE VARIABLES
	protected GUISkin nodeSkin;

	//	SUBCLASSES
	[Serializable]
	public class NE_NodeInput {
		public bool isOccupied = false;
		public bool allowsMultipleInputs = false;
		public NE_NodeBase parentNode;
	}

	[Serializable]
	public class NE_NodeOutput {
		public bool isOccupied = false;
	}

	//	MAIN FUNCTIONS
	public virtual void InitNode() {

	}

	public virtual void UpdateNode(Event e, Rect viewRect) {

	}

	#if UNITY_EDITOR
	public virtual void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin) {
		ProcessEvents(e, viewRect);

		if(isSelected){
			GUI.Box(nodeRect, nodeName, viewSkin.GetStyle("node_selected"));
		} else {
			GUI.Box(nodeRect, nodeName, viewSkin.GetStyle("node_default"));
		}

		EditorUtility.SetDirty(this);
	}

	public virtual void DrawNodeProperties() {
		
	}
	#endif

	void ProcessEvents(Event e, Rect viewRect) {
		if(isSelected) {

			if(e.type == EventType.mouseDrag) {

				if (nodeRect.Contains(e.mousePosition)) {
					nodeRect.x += e.delta.x;
					nodeRect.y += e.delta.y;
				}
			}
		}
	}
}

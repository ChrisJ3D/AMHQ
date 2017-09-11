using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_NodeBase : ScriptableObject {

	public string nodeName;
	public Rect nodeRect;
	public NE_NodeGraph parentGraph;
	public NodeType nodeType;

	protected GUISkin nodeSkin;

	public virtual void InitNode() {

	}

	public virtual void UpdateNode(Event e, Rect viewRect) {

	}

	#if UNITY_EDITOR
	public virtual void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin) {
		ProcessEvents(e, viewRect);

		GUI.Box(nodeRect, nodeName, viewSkin.GetStyle("node_default"));

		EditorUtility.SetDirty(this);
	}
	#endif

	void ProcessEvents(Event e, Rect viewRect) {
		if (viewRect.Contains(e.mousePosition)) {
			if(e.type == EventType.mouseDrag) {
				if (nodeRect.Contains(e.mousePosition)) {
					nodeRect.x += e.delta.x;
					nodeRect.y += e.delta.y;
				}
			}
		}
	}
}

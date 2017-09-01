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

	protected GUISkin nodeSkin;

	public virtual void InitNode() {

	}

	public virtual void UpdateNode(Event e, Rect viewRect) {

	}

	#if UNITY_EDITOR
	public virtual void UpdateNodeGUI(Event e, Rect viewRect) {
		ProcessEvents(e, viewRect);

	}
	#endif

	void ProcessEvents(Event e, Rect viewRect) {

	}

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[Serializable]
public class NE_NodeGraph : ScriptableObject {

	#region Public Variables
	public string graphName = "New graph";
	public List<NE_NodeBase> nodes;
	#endregion

	#region Main Methods

	void OnEnable() {
		if(nodes == null) {
			nodes = new List<NE_NodeBase>();
		}
	}

	// <summary>
	//
	// </summary>
	public void InitGraph() {
		if(nodes.Count > 0) {
			for(int i = 0; i < nodes.Count; i++) {
				nodes[i].InitNode();
			}
		}
	}

	public void UpdateGraph() {
		if(nodes.Count > 0) {
			
		}
	}

	#if UNITY_EDITOR
	public void UpdateGraphGUI(Event e, Rect viewRect) {
		if(nodes.Count > 0) {
			ProcessEvents(e, viewRect);
		}
		EditorUtility.SetDirty(this);

	}
	#endif

	#endregion

	#region Utility Methods
	void ProcessEvents(Event e, Rect viewRect) {
		if(viewRect.Contains(e.mousePosition)) {

		}
	}
	#endregion

}

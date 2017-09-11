using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_ViewBase {

	#region Public Variables
	public string viewTitle;
	public Rect viewRect;
	#endregion

	#region Protected Variables
	protected GUISkin viewSkin;
	protected NE_NodeGraph currentGraph; 
	#endregion

	#region Constructors
	public NE_ViewBase(string title) {
		viewTitle = title;
	}
	#endregion

	#region Main Methods
	public virtual void UpdateView(Rect editorRect, Rect percentageRect, Event e, NE_NodeGraph curGraph) {
		if (viewSkin == null) {
			GetEditorSkin();
			return;
		}

		this.currentGraph = curGraph;

		viewRect = new Rect(editorRect.x * percentageRect.x,
							editorRect.y * percentageRect.y,
							editorRect.width * percentageRect.width,
							editorRect.height * percentageRect.height);

		if(curGraph != null) {
			curGraph.UpdateGraph();
		}


	}

	public void OnEnable () {
		GetEditorSkin();
	}

	public virtual void ProcessEvents(Event e) {

	}
	#endregion

	#region Utility Methods
	protected void GetEditorSkin() {
			viewSkin = (GUISkin)Resources.Load("GUISkins/Editor/NodeEditorSkin");
	}
	#endregion

}

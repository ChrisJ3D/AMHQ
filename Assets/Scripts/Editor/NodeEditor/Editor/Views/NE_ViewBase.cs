using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_ViewBase {

	public string viewTitle;
	public Rect viewRect;

	protected GUISkin viewSkin;
	protected NE_NodeGraph currentGraph; 

	public NE_ViewBase(string title) {
		viewTitle = title;
	}

	public virtual void UpdateView(Rect editorRect, Rect percentageRect, Event e, NE_NodeGraph curGraph) {
		if (!viewSkin) {
			GetEditorSkin();
			return;
		}

		this.currentGraph = curGraph;

		viewRect = new Rect(editorRect.x * percentageRect.x,
							editorRect.y * percentageRect.y,
							editorRect.width * percentageRect.width,
							editorRect.height * percentageRect.height);

		if(curGraph) {
			curGraph.UpdateGraph();
		}
	}

	void OnEnable () {
		GetEditorSkin();
	}

	void OnDisable() {
		AssetDatabase.SaveAssets();
	}

	public virtual void ProcessEvents(Event e) {

	}

	protected void GetEditorSkin() {
			viewSkin = (GUISkin)Resources.Load("GUISkins/Editor/NodeEditorSkin");
	}
}

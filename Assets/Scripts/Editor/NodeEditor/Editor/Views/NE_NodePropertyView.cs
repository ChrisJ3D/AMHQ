using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NE_NodePropertyView : NE_ViewBase {

	public NE_NodePropertyView() : base("Property View") {

	}

	public override void UpdateView(Rect editorRect, Rect percentageRect, Event e, NE_NodeGraph curGraph) {
		base.UpdateView(editorRect, percentageRect, e, curGraph);

		GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("view_bg"));

		GUILayout.BeginArea(viewRect);
		GUILayout.Space(60f);
			if(curGraph) {
				if (curGraph.showProperties) {
					curGraph.selectedNode.DrawNodeProperties();
				} else {
					EditorGUILayout.LabelField("No node selected");
				}
			}
		GUILayout.EndArea();

		ProcessEvents(e);
	}

	public override void ProcessEvents(Event e) {
		base.ProcessEvents(e);

		if (viewRect.Contains(e.mousePosition)) {

		}
	}
}

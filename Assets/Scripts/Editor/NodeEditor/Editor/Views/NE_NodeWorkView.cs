using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_NodeWorkView : NE_ViewBase {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion
	
	#region Constructor
	public NE_NodeWorkView () : base("Node View") {}
	#endregion

	#region Main Methods
	public override void UpdateView(Rect editorRect, Rect percentageRect, Event e, NE_NodeGraph curGraph) {
		base.UpdateView(editorRect, percentageRect, e, curGraph);

		if (curGraph != null) {
			viewTitle = curGraph.graphName;
		} else {
			viewTitle = "No graph";
		}
		
		GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("view_bg"));

		GUILayout.BeginArea(viewRect);
		GUILayout.EndArea();

		ProcessEvents(e);
	}

	public override void ProcessEvents(Event e) {
		base.ProcessEvents(e);

		if (viewRect.Contains(e.mousePosition)) {
			//Debug.Log("Mouse is inside " + viewTitle);

			//	Left mouse button
			if (e.button == 0) {
				if (e.type == EventType.mouseDown) {
					Debug.Log("Left down inside of " + viewTitle);
				}

				if (e.type == EventType.mouseDrag) {
					Debug.Log("Left drag inside of " + viewTitle);
				}

				if (e.type == EventType.mouseUp) {
					Debug.Log("Left up inside of " + viewTitle);
				}
			}

			//	Right mouse button
			if (e.button == 1) {
				if (e.type == EventType.mouseDown) {
					ProcessContextMenu(e);
				}
			}
		}

	}
	#endregion

	#region Utility Methods
	void ProcessContextMenu(Event e) {
		GenericMenu menu = new GenericMenu();
		menu.AddItem(new GUIContent("Create Graph"), false, ContextCallback, "0");
		menu.AddItem(new GUIContent("Load graph"), false, ContextCallback, "1");

		if (currentGraph != null) {
			menu.AddSeparator("");
			menu.AddItem(new GUIContent("Graph/Unload graph"), false, ContextCallback, "2");
		}

		menu.ShowAsContext();
		e.Use();
	}

	void ContextCallback(object obj) {
		switch(obj.ToString()) {
			case "0":
				Debug.Log("NODE VIEW: Creating new graph!");
				NE_PopUpWindow.InitNodePopUp();
				break;

			case "1":
				Debug.Log("Loading graph");
				break;

			case "2":
				Debug.Log("Unloading graph");
				break;
			
			default:
				break;
		}
	}
	#endregion
}

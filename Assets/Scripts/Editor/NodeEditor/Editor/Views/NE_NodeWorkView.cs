using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

		//	This code grabs the internal graph name and displays it
		//	in the top label field. I opted to instead show the name
		//	of the current scene.

		// if (curGraph != null) {
		// 	viewTitle = curGraph.graphName;
		// } else {
		// 	viewTitle = "No graph";
		// }

		viewTitle = SceneManager.GetActiveScene().name;
		
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

		menu.AddItem(new GUIContent("Nodes/Dialogue"), false, ContextCallback, "3");
		menu.AddItem(new GUIContent("Nodes/Question"), false, ContextCallback, "3");
		menu.AddSeparator("Nodes/");
		menu.AddItem(new GUIContent("Nodes/Get Stat"), false, ContextCallback, "3");
		menu.AddItem(new GUIContent("Nodes/Get Date"), false, ContextCallback, "3");
		menu.AddItem(new GUIContent("Nodes/Get Affection"), false, ContextCallback, "3");
		menu.AddSeparator("Nodes/");
		menu.AddItem(new GUIContent("Nodes/Set Stat"), false, ContextCallback, "3");
		menu.AddItem(new GUIContent("Nodes/Set Date"), false, ContextCallback, "3");
		menu.AddItem(new GUIContent("Nodes/Set Affection"), false, ContextCallback, "3");
		menu.AddSeparator("Nodes/");
		menu.AddItem(new GUIContent("Nodes/Set Background Image"), false, ContextCallback, "3");
		menu.AddItem(new GUIContent("Nodes/Set Background Music"), false, ContextCallback, "3");
		menu.AddItem(new GUIContent("Nodes/Play Sound"), false, ContextCallback, "3");
		menu.AddItem(new GUIContent("Nodes/Play Effect"), false, ContextCallback, "3");
		menu.AddItem(new GUIContent("Nodes/Delay"), false, ContextCallback, "3");
		menu.AddSeparator("Nodes/");
		menu.AddItem(new GUIContent("Nodes/Load Scene"), false, ContextCallback, "3");

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

			case "3":
				Debug.LogError("Node not implemented");
				break;

			case "4":
				Debug.LogError("Node not implemented");
				break;

			case "5":
				Debug.LogError("Node not implemented");
				break;

			case "6":
				Debug.LogError("Node not implemented");
				break;

			case "7":
				Debug.LogError("Node not implemented");
				break;

			case "8":
				Debug.LogError("Node not implemented");
				break;

			case "9":
				Debug.LogError("Node not implemented");
				break;

			case "10":
				Debug.LogError("Node not implemented");
				break;

			case "11":
				Debug.LogError("Node not implemented");
				break;

			case "12":
				Debug.LogError("Node not implemented");
				break;
			
			default:
				Debug.LogError("Node not implemented");
				break;
		}
	}
	#endregion
}

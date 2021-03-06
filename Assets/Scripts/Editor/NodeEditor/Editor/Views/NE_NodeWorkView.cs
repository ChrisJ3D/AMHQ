﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_NodeWorkView : NE_ViewBase {
	//	The NodeWorkView is tasked with displaying the contents of a NE_NodeGraph, and handling any input events

	Vector2 mousePosition;
	int hoveredNodeID = 0;

	public NE_NodeWorkView () : base("Node View") {}

	public override void UpdateView(Rect editorRect, Rect percentageRect, Event e, NE_NodeGraph curGraph) {
		base.UpdateView(editorRect, percentageRect, e, curGraph);

		if (curGraph) {
			viewTitle = curGraph.graphName;
		} else {
			viewTitle = "No graph";
		}
		
		GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("view_bg"));

		GUILayout.BeginArea(viewRect);
		if (currentGraph) {
			currentGraph.UpdateGraphGUI(e, viewRect, viewSkin);
		}
		GUILayout.EndArea();

		ProcessEvents(e);
	}

	public override void ProcessEvents(Event e) {
		base.ProcessEvents(e);

		if (viewRect.Contains(e.mousePosition)) {
			

			//	Left mouse button
			if (e.button == 0) {

				if (e.type == EventType.MouseDrag) {
						//	TODO: Add code for creating a marquee here
				}
			}

			//	Right mouse button
			if (e.button == 1) {
				bool isOverNode = false;
				bool isOverConnector = false;
				if (e.type == EventType.MouseDown) {
					mousePosition = e.mousePosition;

					if (currentGraph) {
						
						if (currentGraph.nodes.Count > 0) {
							
							foreach(NE_NodeBase node in currentGraph.nodes) {
								
								if(node.nodeRect.Contains(mousePosition)) {
									node.OnClicked();
									isOverNode = true;
									hoveredNodeID = currentGraph.nodes.IndexOf(node);
								} else {
									foreach (NE_NodeConnectorBase input in node.inputs) {
										if (input.connectorRect.Contains(mousePosition)) {
											isOverConnector = true;
											input.OnClicked();
										}
									}
								}
							}
						}
					}

					if (isOverNode) {
						ProcessContextMenu(e, 1);
					} else if (isOverConnector) {
						
					} else {
						ProcessContextMenu(e, 0);
					}

				}
			}
		}
	}

	void ProcessContextMenu(Event e, int contextID) {
		GenericMenu menu = new GenericMenu();

		if (contextID == 0) {		
			menu.AddItem(new GUIContent("Graph/Create Graph"), false, ContextCallback, "0");
			menu.AddItem(new GUIContent("Graph/Load graph"), false, ContextCallback, "1");
			
			if (currentGraph != null) {
				menu.AddSeparator("");
				menu.AddItem(new GUIContent("Graph/Unload graph"), false, ContextCallback, "2");
			}

			menu.AddItem(new GUIContent("Nodes/Dialogue"), false, ContextCallback, "3");
			menu.AddItem(new GUIContent("Nodes/Question"), false, ContextCallback, "4");

			menu.AddSeparator("Nodes/");
			menu.AddItem(new GUIContent("Nodes/Get Stat"), false, ContextCallback, "5");
			menu.AddItem(new GUIContent("Nodes/Get Date"), false, ContextCallback, "6");
			menu.AddItem(new GUIContent("Nodes/Get Affection"), false, ContextCallback, "7");

			menu.AddSeparator("Nodes/");
			menu.AddItem(new GUIContent("Nodes/Set Stat"), false, ContextCallback, "8");
			menu.AddItem(new GUIContent("Nodes/Set Date"), false, ContextCallback, "9");
			menu.AddItem(new GUIContent("Nodes/Set Affection"), false, ContextCallback, "10");

			menu.AddSeparator("Nodes/");
			menu.AddItem(new GUIContent("Nodes/Get Item in Inventory"), false, ContextCallback, "11");
			menu.AddItem(new GUIContent("Nodes/Add Item to Inventory"), false, ContextCallback, "12");
			menu.AddItem(new GUIContent("Nodes/Clear Inventory"), false, ContextCallback, "13");

			menu.AddSeparator("Nodes/");
			menu.AddItem(new GUIContent("Nodes/Set Background Image"), false, ContextCallback, "14");
			menu.AddItem(new GUIContent("Nodes/Set Background Music"), false, ContextCallback, "15");
			menu.AddItem(new GUIContent("Nodes/Play Sound"), false, ContextCallback, "16");
			menu.AddItem(new GUIContent("Nodes/Play Effect"), false, ContextCallback, "17");
			menu.AddItem(new GUIContent("Nodes/Delay"), false, ContextCallback, "18");

			menu.AddSeparator("Nodes/");
			menu.AddItem(new GUIContent("Nodes/Load Scene"), false, ContextCallback, "20");

			menu.AddItem(new GUIContent("Arithmetic/Float"), false, ContextCallback, "21");
			menu.AddItem(new GUIContent("Arithmetic/Integer"), false, ContextCallback, "22");
			menu.AddItem(new GUIContent("Arithmetic/Boolean"), false, ContextCallback, "23");

			menu.AddSeparator("Arithmetic/");
			menu.AddItem(new GUIContent("Arithmetic/Add"), false, ContextCallback, "24");
			menu.AddItem(new GUIContent("Arithmetic/Subtract"), false, ContextCallback, "25");
			menu.AddItem(new GUIContent("Arithmetic/Multiply"), false, ContextCallback, "26");
			menu.AddItem(new GUIContent("Arithmetic/Divide"), false, ContextCallback, "27");
		}

		if (contextID == 1) {
			
			menu.AddItem(new GUIContent("Copy"), false, ContextCallback, "30");
			menu.AddItem(new GUIContent("Paste"), false, ContextCallback, "31");
			menu.AddItem(new GUIContent("Cut"), false, ContextCallback, "32");
			menu.AddItem(new GUIContent("Duplicate"), false, ContextCallback, "33");
			menu.AddItem(new GUIContent("Delete"), false, ContextCallback, "34");
		}

		menu.ShowAsContext();
		e.Use();
	}

	void ContextCallback(object obj) {
		switch(obj.ToString()) {
			case "0":
				NE_PopUpWindow.InitNodePopUp();
				break;

			case "1":
				NE_NodeUtils.LoadGraph();
				break;

			case "2":
				NE_NodeUtils.UnloadGraph();
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

			case "21":
				NE_NodeUtils.CreateNode(currentGraph, NodeType.Float, mousePosition);
				break;

			case "22":
				NE_NodeUtils.CreateNode(currentGraph, NodeType.Integer, mousePosition);
				break;

			case "23":
				NE_NodeUtils.CreateNode(currentGraph, NodeType.Boolean, mousePosition);
				break;

			case "24":
				NE_NodeUtils.CreateNode(currentGraph, NodeType.Add, mousePosition);
				break;

			case "25":
				NE_NodeUtils.CreateNode(currentGraph, NodeType.Subtract, mousePosition);
				break;

			case "26":
				NE_NodeUtils.CreateNode(currentGraph, NodeType.Multiply, mousePosition);
				break;

			case "27":
				NE_NodeUtils.CreateNode(currentGraph, NodeType.Divide, mousePosition);
				break;
			
			case "33":
				NE_NodeUtils.DuplicateNode(hoveredNodeID, currentGraph);
				break;

			case "34":
				NE_NodeUtils.DeleteNode(hoveredNodeID, currentGraph);
				break;
			
			default:
				Debug.LogError("Node not implemented");
				break;
		}
	}
}

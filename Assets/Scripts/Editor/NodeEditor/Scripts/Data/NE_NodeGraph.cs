﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[Serializable]
public class NE_NodeGraph : ScriptableObject {

	public string graphName = "New Graph";
	public List<NE_NodeBase> nodes;
	public NE_NodeBase selectedNode;

	public bool wantsConnection = false;
	public NE_NodeBase connectionNode;
	public bool showProperties = false;

	void OnEnable() {
		graphName = SceneManager.GetActiveScene().name;
		
		if(nodes == null) {
			nodes = new List<NE_NodeBase>();
		}
	}

	public void InitGraph() {
		foreach(NE_NodeBase node in nodes) {
			node.InitNode();
		}
	}

	public void UpdateGraph() {
		if(nodes.Count > 0) {
			
		}
	}

	#if UNITY_EDITOR
	public void UpdateGraphGUI(Event e, Rect viewRect, GUISkin viewSkin) {
		if(nodes.Count > 0) {
			ProcessEvents(e, viewRect);
			for(int i = 0; i < nodes.Count; i++) {
				nodes[i].UpdateNodeGUI(e, viewRect, viewSkin);
			}
		}

		if(wantsConnection) {
			
			if(connectionNode) {
					DrawConnectionToMouse(e.mousePosition);
				}
		}

		if (e.type == EventType.Layout) {
			if(selectedNode) {
				showProperties = true;
			}
		}
		EditorUtility.SetDirty(this);

	}	
	#endif

	void ProcessEvents(Event e, Rect viewRect) {
		//Get ready for some nesting...

		if(viewRect.Contains(e.mousePosition)) {

			if(e.button == 0) {

				if(e.type == EventType.MouseDown) {
					DeselectAllNodes();

					bool setNode = false;
					selectedNode = null;
					showProperties = false;

					foreach(NE_NodeBase node in nodes) {
							if(node.nodeRect.Contains(e.mousePosition)) {
								node.isSelected = true;
								selectedNode = node;
								setNode = true;
							}
						}
					
					if(!setNode) {
						DeselectAllNodes();
						}

					if(wantsConnection) {
						wantsConnection = false;
					}
				}
			}
		}
	}

	void DeselectAllNodes() {
		foreach(NE_NodeBase node in nodes) {
			node.isSelected = false;
		}
	}

	void DrawConnectionToMouse(Vector2 mousePosition) {
		Vector3 origin = new Vector3(connectionNode.nodeRect.x + connectionNode.nodeRect.width,
										connectionNode.nodeRect.y + connectionNode.nodeRect.height * 0.5f);
		Vector3 destination = new Vector3(mousePosition.x, mousePosition.y);

		Handles.BeginGUI();
		Handles.color = Color.white;
		Handles.DrawLine(origin, destination);
		Handles.EndGUI();
	}
}

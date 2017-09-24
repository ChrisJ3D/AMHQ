using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class NE_NodeConnectorBase : ScriptableObject {

	public int index;
	public bool isOccupied;
	public bool allowsMultipleMatches;
	public NE_NodeBase parentNode;
	public Vector2 size = new Vector2(24f, 24f);
	public Vector2 position = new Vector2(0.0f, 0.0f);
	public Rect connectorRect;
	public GUIStyle connectorSkin;

	public bool wantsConnection = false;
	public NE_NodeConnectorBase inputConnector;

	public virtual void OnEnable() {
		GetConnectionPosition();
	}
	
	public virtual void GetConnectionPosition() {

	}

	public virtual void DrawGUI() {
		connectorRect.position = position;
		connectorRect.size = size;

		if (GUI.Button(connectorRect, "", connectorSkin)) {
			OnClicked();
		}
	}

	public virtual void OnClicked() {

		if (parentNode.parentGraph) {
			parentNode.parentGraph.wantsConnection = true;
			parentNode.parentGraph.connectionNode = parentNode;
			parentNode.parentGraph.connectionMatch = this;
		}

		Debug.Log("input " + index + " clicked, wantsConnection is " + wantsConnection, this);
	}

	void ProcessEvents(Event e) {
		// if(isSelected) {

		// 	if(e.type == EventType.mouseDrag) {

		// 		if (nodeRect.Contains(e.mousePosition)) {
		// 			nodeRect.x += e.delta.x;
		// 			position.x += e.delta.x;
		// 			nodeRect.y += e.delta.y;
		// 			position.y += e.delta.y;
		// 		}
		// 	}
		// }
	}

	public virtual void DrawConnections() {
		if(inputConnector && isOccupied) {
			NE_NodeUtils.DrawLine(inputConnector.GetConnectionLinePosition(), this.GetConnectionLinePosition(), "");
		}
	}

	public virtual Vector3 GetConnectionLinePosition() {
		Vector3 connectionPosition = new Vector3();
		return connectionPosition;
	}
}

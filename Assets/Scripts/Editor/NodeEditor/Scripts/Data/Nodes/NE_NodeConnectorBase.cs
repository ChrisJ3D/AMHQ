using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class NE_NodeConnectorBase : ScriptableObject {

	public int index;
	public bool isOccupied = false;
	public bool allowsMultipleMatches = false;
	public NE_NodeBase parentNode = null;
	public Vector2 size = new Vector2(24f, 24f);
	public Vector2 position = new Vector2(0.0f, 0.0f);

	public NE_NodeOutput match = null;
	public bool wantsConnection = false;

	public virtual void OnEnable() {
		GetConnectionPosition();
	}
	
	public virtual void GetConnectionPosition() {

	}

	public virtual void DrawConnections(Event e) {
		if(match) {
			NE_NodeUtils.DrawLine(match.GetConnectionLinePosition(), this.GetConnectionLinePosition(), "");
		}
	}

	public virtual Vector3 GetConnectionLinePosition() {
		Vector3 connectionPosition = new Vector3();
		return connectionPosition;
	}
}

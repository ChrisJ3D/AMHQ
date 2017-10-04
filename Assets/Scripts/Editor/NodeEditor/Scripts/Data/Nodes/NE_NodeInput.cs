using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class NE_NodeInput : NE_NodeConnectorBase {
	
	public override void OnEnable() {
		base.OnEnable();
	}

	public override void OnClicked() {
		if (parentNode.parentGraph.connectionMatch) {
			if (!parentNode.CheckIfAlreadyConnected(parentNode.parentGraph.connectionNode)) {
				inputConnector = parentNode.parentGraph.connectionMatch;
				parentNode.parentGraph.connectionMatch.isOccupied = true;
				isOccupied = true;
				parentNode.connectedNodes.Add(parentNode.parentGraph.connectionNode);
				parentNode.parentGraph.connectionNode = null;
				parentNode.parentGraph.connectionMatch = null;
				
			}
		}
	}

	public override void GetConnectionPosition() {
		if (parentNode) {
			float top = parentNode.nodeRect.y + (size.y * 0.5f);
			float center = top + (parentNode.nodeRect.height * 0.5f) - size.y;
			
			float fraction = (index + 1) / ((float)parentNode.numberOfInputs + 1);
			float fractionOffset = (fraction - 0.5f) * 2f;
			
			position.y = center + (fractionOffset * (parentNode.nodeRect.height /2));

			position.x = parentNode.nodeRect.x - size.x;
			connectorSkin = parentNode.nodeSkin.GetStyle("node_input");
		}
	}
}

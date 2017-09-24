using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class NE_NodeOutput : NE_NodeConnectorBase {

	public override void OnEnable() {
		base.OnEnable();
	}

	public override void OnClicked() {
		if (parentNode.parentGraph) {
			parentNode.parentGraph.wantsConnection = true;
			parentNode.parentGraph.connectionNode = parentNode;
			parentNode.parentGraph.connectionMatch = this;
		}
	}

	public override void GetConnectionPosition() {
		if (parentNode) {
			float top = parentNode.nodeRect.y + (size.y * 0.5f);
			float center = top + (parentNode.nodeRect.height * 0.5f) - size.y;
			
			float fraction = (index + 1) / ((float)parentNode.numberOfOutputs + 1);
			float fractionOffset = (fraction - 0.5f) * 2f;
			
			position.y = center + (fractionOffset * (parentNode.nodeRect.height /2f));

			position.x = parentNode.nodeRect.x + parentNode.nodeRect.width;
			connectorSkin = parentNode.nodeSkin.GetStyle("node_output");
		}
	}
}

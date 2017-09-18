using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class NE_NodeOutput : NE_NodeConnectorBase {

	public override void GetConnectionPosition() {
		if (parentNode) {
			float top = parentNode.nodeRect.y + (size.y * 0.5f);
			float center = top + (parentNode.nodeRect.height * 0.5f) - size.y;
			
			float fraction = (index + 1) / ((float)parentNode.numberOfOutputs + 1);
			float fractionOffset = (fraction - 0.5f) * 2f;
			
			position.y = center + (fractionOffset * (parentNode.nodeRect.height /2f));

			position.x = parentNode.nodeRect.x + parentNode.nodeRect.width;
		}
	}

	public override Vector3 GetConnectionLinePosition() {
		Vector3 connectionPosition = new Vector3();
		connectionPosition.x = position.x + size.x * 0.5f;
		connectionPosition.y = position.y + size.y * 0.5f;

		return connectionPosition;
	}
}

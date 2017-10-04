using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_BooleanNode : NE_NodeBase {

    //  CONSTRUCTORS
    public NE_BooleanNode() {
        numberOfInputs = 0;
        numberOfOutputs = 1;
    }

    //  MAIN FUNCTIONS
    public override void InitNode() {
        base.InitNode();
        nodeType = NodeType.Boolean;
        nodeRect = new Rect(position.x,position.y,150f,65f);
    }

    public override void Evaluate() {
        if (nodeValue == null) {
            nodeValue = false;
        }
    }

    public override void DrawNodeProperties() {
        base.DrawNodeProperties();
        if (nodeValue != null) {
            nodeValue = EditorGUILayout.Toggle("Boolean Value: ", (bool)nodeValue);
        }
    }
}
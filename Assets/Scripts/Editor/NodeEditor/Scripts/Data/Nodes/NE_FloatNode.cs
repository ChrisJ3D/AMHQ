using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_FloatNode : NE_NodeBase {

    //  CONSTRUCTORS
    public NE_FloatNode() {
        numberOfInputs = 0;
        numberOfOutputs = 1;

        nodeValue = 0.0f;
    }

    //  MAIN FUNCTIONS
    public override void InitNode() {
        base.InitNode();
        nodeType = NodeType.Float;
        nodeRect = new Rect(position.x,position.y,150f,65f);
    }

    public override void Evaluate() {
        
    }

    public override void DrawNodeProperties() {
        base.DrawNodeProperties();
        nodeValue = EditorGUILayout.FloatField("Float Value: ", (float)nodeValue);
    }
}
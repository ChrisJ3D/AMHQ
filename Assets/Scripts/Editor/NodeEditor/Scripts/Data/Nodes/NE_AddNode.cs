using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_AddNode : NE_NodeBase {


    //  CONSTRUCTORS
    public NE_AddNode() {
        numberOfInputs = 2;
        numberOfOutputs = 1;
    }

    //  MAIN FUNCTIONS
    public override void InitNode() {
        base.InitNode();
        nodeType = NodeType.Add;
        nodeRect = new Rect(10f,10f,200,65f);
    }

    public override void Evaluate() {
        nodeValue = 0.0f;

        foreach(NE_NodeInput i in inputs) {
                NE_NodeBase entry = i.parentNode;
                nodeValue = (float)nodeValue + entry.EvaluateAsFloat();
        }
    }

    public override void DrawNodeProperties() {
        base.DrawNodeProperties();

        EditorGUILayout.FloatField("Sum: ", this.EvaluateAsFloat());
    }
}
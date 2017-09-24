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
        nodeRect = new Rect(position.x,position.y,200,105f);
    }

    public override void Evaluate() {
        Debug.Log("Evaluating");
        nodeValue = 0.0f;

        if(inputs != null) {
            if(inputs.Count > 0) {
                foreach(NE_NodeInput i in inputs) {
                    if (i.isOccupied) {
                        float inputValue = i.inputConnector.parentNode.EvaluateAsFloat();
                        nodeValue = (float)nodeValue + inputValue;
                    }
                }
            }
        }
    }

    public override void DrawNodeProperties() {
        base.DrawNodeProperties();
        if (nodeValue != null) {
            EditorGUILayout.FloatField("Sum: ", (float)nodeValue);
        }
    }
}
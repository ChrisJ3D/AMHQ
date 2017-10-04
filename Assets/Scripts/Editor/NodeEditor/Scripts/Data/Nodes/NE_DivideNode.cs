using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_DivideNode : NE_NodeBase {

    //  CONSTRUCTORS
    public NE_DivideNode() {
        numberOfInputs = 2;
        numberOfOutputs = 1;
    }

    //  MAIN FUNCTIONS
    public override void InitNode() {
        base.InitNode();
        nodeType = NodeType.Divide;
        nodeRect = new Rect(position.x,position.y,200,105f);
    }

    public override void Evaluate() {
        nodeValue = 0.0f;

        if(inputs != null) {
            if(inputs.Count > 0) {
                float a = 0.0f;
                float b = 0.0f;

                if (inputs[0].inputConnector != null) {
                    a = inputs[0].inputConnector.parentNode.EvaluateAsFloat();
                }

                if (inputs[1].inputConnector != null) {
                    b = inputs[1].inputConnector.parentNode.EvaluateAsFloat();
                }

                nodeValue = a / b;
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
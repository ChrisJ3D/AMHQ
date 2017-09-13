using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_AddNode : NE_NodeBase {
    
    //  PUBLIC VARIABLES
    public float nodeSum;
    public NE_NodeOutput output;
    public NE_NodeInput inputA;
    public NE_NodeInput inputB;

    //  CONSTRUCTORS
    public NE_AddNode() {
        output = new NE_NodeOutput();
        inputA = new NE_NodeInput();
        inputB = new NE_NodeInput();
    }

    //  MAIN FUNCTIONS
    public override void InitNode() {
        base.InitNode();
        nodeType = NodeType.Add;
        nodeRect = new Rect(10f,10f,200,65f);
    }

    public override void UpdateNode(Event e, Rect viewRect) {
        base.UpdateNode(e, viewRect);
    }

    #if UNITY_EDITOR
    public override void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin) {
        base.UpdateNodeGUI(e, viewRect, viewSkin);

        //  OUTPUT
        if(GUI.Button(new Rect(nodeRect.x + nodeRect.width, nodeRect.y + (nodeRect.height * 0.5f) - 12f, 24f, 24f), "", viewSkin.GetStyle("node_output"))) {
            if (parentGraph != null) {
                parentGraph.wantsConnection = true;
                parentGraph.connectionNode = this;
            }
        }

        //  INPUT A
        if(GUI.Button(new Rect(nodeRect.x - 24f, nodeRect.y + ((nodeRect.height * 0.1f) * 2) - 8f, 24f, 24f), "", viewSkin.GetStyle("node_input"))) {
            if (parentGraph != null) {
                inputA.parentNode = parentGraph.connectionNode;
                inputA.isOccupied = inputA.parentNode != null ? true : false;

                parentGraph.wantsConnection = false;
                parentGraph.connectionNode = null;
            }
        }

        //  INPUT B
        if(GUI.Button(new Rect(nodeRect.x - 24f, nodeRect.y + (nodeRect.height * 0.75f) - 12f, 24f, 24f), "", viewSkin.GetStyle("node_input"))) {
            if (parentGraph != null) {
                inputB.parentNode = parentGraph.connectionNode;
                inputB.isOccupied = inputB.parentNode != null ? true : false;

                parentGraph.wantsConnection = false;
                parentGraph.connectionNode = null;
            }
        }

        DrawInputLines();

        if (inputA.isOccupied && inputB.isOccupied) {
            NE_FloatNode nodeA = (NE_FloatNode)inputA.parentNode;
            NE_FloatNode nodeB = (NE_FloatNode)inputB.parentNode;

            if (nodeA && nodeB) {
                 nodeSum = nodeA.nodeValue + nodeB.nodeValue;
            } else {
                nodeSum = 0.0f;
            }
        }
    }
    #endif

    void DrawInputLines() {
        if (inputA.parentNode && inputA.isOccupied) {
            DrawLine(inputA, 0);
        } else {
            inputA.isOccupied = false;
        }

        if (inputB.parentNode && inputB.isOccupied) {
            DrawLine(inputB, 1);
        } else {
            inputB.isOccupied = false;
        }

    }

    void DrawLine(NE_NodeInput input, int inputID) {
        Vector3 origin = new Vector3 (input.parentNode.nodeRect.x + input.parentNode.nodeRect.width + 10f,
                                        input.parentNode.nodeRect.y + input.parentNode.nodeRect.height * 0.5f);
        Vector3 destination = new Vector3(nodeRect.x - 10f, 
                                            (nodeRect.y + nodeRect.height * 0.33f + (inputID * 25)));

        Handles.BeginGUI();
        Handles.color = Color.white;
        Handles.DrawLine(origin, destination);
        Handles.EndGUI();
    }

    public override void DrawNodeProperties() {
        base.DrawNodeProperties();

        EditorGUILayout.FloatField("Sum: ", nodeSum);
    }
}
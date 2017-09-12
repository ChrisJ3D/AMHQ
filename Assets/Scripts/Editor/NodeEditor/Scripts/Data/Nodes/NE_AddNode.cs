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
        if(GUI.Button(new Rect(nodeRect.x + nodeRect.width, nodeRect.y + (nodeRect.height * 0.5f) - 12f, 
                                24f, 24f),
                                 "", viewSkin.GetStyle("node_output"))) {
                                     Debug.Log("Output selected");
                                 }

        //  INPUT A
        if(GUI.Button(new Rect(nodeRect.x - 24f, nodeRect.y + (nodeRect.height * 0.5f) - 12f, 
                                24f, 24f), "", viewSkin.GetStyle("node_output"))) {
                                     Debug.Log("Output selected");
                                 }

        //  INPUT B
        if(GUI.Button(new Rect(nodeRect.x - 24f, nodeRect.y + (nodeRect.height * 0.5f) - 12f, 
                                24f, 24f),
                                 "", viewSkin.GetStyle("node_output"))) {
                                     Debug.Log("Output selected");
                                 }
    }
    #endif
}
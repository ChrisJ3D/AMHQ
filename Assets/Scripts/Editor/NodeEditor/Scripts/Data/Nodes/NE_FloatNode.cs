using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_FloatNode : NE_NodeBase {
    
    //  PUBLIC VARIABLES
    public float nodeValue;
    public NE_NodeOutput output;


    //  CONSTRUCTORS
    public NE_FloatNode() {
        output = new NE_NodeOutput();
    }

    //  MAIN FUNCTIONS
    public override void InitNode() {
        base.InitNode();
        nodeType = NodeType.Float;
        nodeRect = new Rect(10f,10f,150f,65f);
    }

    public override void UpdateNode(Event e, Rect viewRect) {
        base.UpdateNode(e, viewRect);
    }

    #if UNITY_EDITOR
    public override void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin) {
        base.UpdateNodeGUI(e, viewRect, viewSkin);

        if(GUI.Button(new Rect(nodeRect.x + nodeRect.width, nodeRect.y + (nodeRect.height * 0.5f) - 12f, 
                                24f, 24f),
                                 "", viewSkin.GetStyle("node_output"))) {
                                     Debug.Log("Output selected");
                                 }
    }
    #endif
}
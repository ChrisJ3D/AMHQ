using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_FloatNode : NE_NodeBase {
    public float nodeValue;

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
    }
    #endif
}
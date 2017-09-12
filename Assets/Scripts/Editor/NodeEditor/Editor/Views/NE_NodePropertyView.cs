using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NE_NodePropertyView : NE_ViewBase {
	
	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Constructor
	public NE_NodePropertyView() : base("Property View") {}
	#endregion

	#region Main Methods
	public override void UpdateView(Rect editorRect, Rect percentageRect, Event e, NE_NodeGraph curGraph) {
		base.UpdateView(editorRect, percentageRect, e, curGraph);
		
		GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("view_bg"));

		GUILayout.BeginArea(viewRect);
		GUILayout.EndArea();

		ProcessEvents(e);
	}

	public override void ProcessEvents(Event e) {
		base.ProcessEvents(e);

		if (viewRect.Contains(e.mousePosition)) {

		}
	}
	#endregion

	#region Utility Methods
	#endregion
}

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
	public override void UpdateView(Rect editorRect, Rect percentageRect) {
		base.UpdateView(editorRect, percentageRect);
		
		GUI.Box(viewRect, viewTitle);

		GUILayout.BeginArea(viewRect);
		EditorGUILayout.LabelField("This is a label");
		GUILayout.EndArea();
	}
	#endregion

	#region Utility Methods
	#endregion
}

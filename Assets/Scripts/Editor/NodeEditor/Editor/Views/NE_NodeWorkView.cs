using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class NE_NodeWorkView : NE_ViewBase {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion
	
	#region Constructor
	public NE_NodeWorkView () : base("Work View") {}
	#endregion

	#region Main Methods
	public override void UpdateView(Rect editorRect, Rect percentageRect) {
		base.UpdateView(editorRect, percentageRect);
		//Debug.Log("Updating" + viewTitle);
		
		GUI.Box(viewRect, viewTitle);

		GUILayout.BeginArea(viewRect);
		EditorGUILayout.LabelField("This is a label");
		GUILayout.EndArea();
	}
	#endregion

	#region Utility Methods
	#endregion
}

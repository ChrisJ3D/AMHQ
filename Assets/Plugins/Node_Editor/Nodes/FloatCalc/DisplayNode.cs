using System;
using System.Collections;
using UnityEngine;

using NodeEditorFramework;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Float/Display")]
	public class DisplayNode : Node 
	{
		public const string ID = "displayNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Display Node"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (130, 120); } }

		private float value;

		[ValueConnectionKnob("Value", Direction.In, "Number")]
		public ValueConnectionKnob inputKnob;
		
		public override void NodeGUI () 
		{
			GUILayout.Space(5);
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			inputKnob.DisplayLayout (new GUIContent(""));
			GUILayout.EndVertical();

			GUILayout.Space(-30);

			GUILayout.BeginVertical();
			GUILayout.Label("X: " + value.ToString());
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
		}
		
		public override bool Calculate () 
		{
			value = inputKnob.GetValue<float> ();
			return true;
		}
	}
}
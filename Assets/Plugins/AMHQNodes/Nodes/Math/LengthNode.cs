using System;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Length")]
	public class LengthNode : Node 
	{
		public const string ID = "LengthNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Length"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 50); } }

		[ValueConnectionKnob("Vector", Direction.In, "Number")]
		public ValueConnectionKnob inputKnob;
		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number length = new Number();
		protected string label = "";
		
		public override void NodeGUI () 
		{
			GUILayout.Space(5f);

			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			inputKnob.DisplayLayout ();

			GUILayout.EndVertical ();
			GUILayout.BeginVertical ();
			
			outputKnob.DisplayLayout (new GUIContent(label));
			
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
			
		}
		
		public override bool Calculate () 
		{
			if (inputKnob.connected()) {
				Number v = inputKnob.GetValue<Number>();
				length =  Mathf.Sqrt((v.x * v.x) + (v.y * v.y) + (v.z * v.z) + (v.w * v.w));
				outputKnob.SetValue<Number> (length);
			}

			label = length.ToString();
				
			return true;
		}
	}
}
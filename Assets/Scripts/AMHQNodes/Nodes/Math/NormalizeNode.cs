using System;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Normalize")]
	public class NormalizeNode : Node 
	{
		public const string ID = "NormalizeNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Normalize"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 50); } }

		[ValueConnectionKnob("Vector", Direction.In, "Number")]
		public ValueConnectionKnob inputKnob;
		[ValueConnectionKnob("Normalized", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number normalizedVector = new Number();
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
			normalizedVector = 0f;

			float length = 1.0f;
			
			if (inputKnob.connected()) {
				normalizedVector = inputKnob.GetValue<Number>();
				length =  Mathf.Sqrt((normalizedVector.x * normalizedVector.x) + (normalizedVector.y * normalizedVector.y) + (normalizedVector.z * normalizedVector.z) + (normalizedVector.w * normalizedVector.w));
			}

			outputKnob.SetValue<Number> (normalizedVector / length);

			label = outputKnob.GetValue<Number> ().ToString();

			return true;
		}
	}
}
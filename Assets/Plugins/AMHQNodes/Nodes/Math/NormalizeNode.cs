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
		}
		
		public override bool Calculate () 
		{
			if (inputKnob.connected()) {
				float length = 0.0f;
				Number v = inputKnob.GetValue<Number>();
				length =  Mathf.Sqrt((v.x * v.x) + (v.y * v.y) + (v.z * v.z) + (v.w * v.w));
				outputKnob.SetValue<Number> (v / length);
			}

			label = outputKnob.GetValue<Number> ().ToString();

			return true;
		}
	}
}
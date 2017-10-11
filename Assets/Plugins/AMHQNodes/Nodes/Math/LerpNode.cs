using System;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Lerp")]
	public class LerpNode : Node 
	{
		public const string ID = "LerpNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Lerp"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 100); } }
		public override string description { get { return "The Lerp node (short for \"linear interpolation\") blends between a min and a max value based on its input. The input goes from 0 to 1."; } }

		[ValueConnectionKnob("A", Direction.In, "Number")]
		public ValueConnectionKnob aKnob;

		[ValueConnectionKnob("B", Direction.In, "Number")]
		public ValueConnectionKnob bKnob;

		[ValueConnectionKnob("Blend", Direction.In, "Number")]
		public ValueConnectionKnob blendKnob;

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number blend = new Number();
		public Number a = new Number();
		public Number b = new Number();
		public Number output = new Number();

		protected string label = "";
		
		public override void NodeGUI () 
		{
			GUILayout.Space(5f);

			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			aKnob.DisplayLayout ();
			bKnob.DisplayLayout();
			blendKnob.DisplayLayout();

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
			blend = 0.5f;
			a = 0f;
			b = 0f;

			output = 0f;

			if (aKnob.connected()) {
				a = aKnob.GetValue<Number>();
			}

			if (bKnob.connected()) {
				b = bKnob.GetValue<Number>();
			}

			if (blendKnob.connected()) {
				blend = blendKnob.GetValue<Number>();
			}

			output = Mathf.Lerp(a,b,blend);

			outputKnob.SetValue<Number>(output);

			label = output.ToStringShort();
				
			return true;
		}
	}
}
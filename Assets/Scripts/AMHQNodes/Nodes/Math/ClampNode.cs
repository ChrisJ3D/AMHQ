using System;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Clamp")]
	public class ClampNode : Node 
	{
		public const string ID = "ClampNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Clamp"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (150, 100); } }
		public override string description { get { return "The Clamp node limits an input to never go above or below the designated min and max values. If the input is lower than the min or higher than the max, the respective min/max value will be output instead."; } }


		[ValueConnectionKnob("Input", Direction.In, "Number")]
		public ValueConnectionKnob inputKnob;
		[ValueConnectionKnob("Min", Direction.In, "Number")]
		public ValueConnectionKnob minClampKnob;
		[ValueConnectionKnob("Max", Direction.In, "Number")]
		public ValueConnectionKnob maxClampKnob;
		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number input = new Number();
		public Number minValue = new Number();
		public Number maxValue = new Number();
		protected string label = "";
		
		public override void NodeGUI () 
		{
			GUILayout.Space(5f);

			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();

			inputKnob.DisplayLayout ();
			minClampKnob.DisplayLayout();
			maxClampKnob.DisplayLayout();

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
			input = 0f;
			minValue = 0f;
			maxValue = 1000f;

			if (minClampKnob.connected()) {
				minValue = minClampKnob.GetValue<Number>();
			}

			if (maxClampKnob.connected()) {
				maxValue = maxClampKnob.GetValue<Number>();
			}

			if (inputKnob.connected()) {
				input = inputKnob.GetValue<Number>();
			}

			input = Mathf.Clamp(input, minValue, maxValue);

			outputKnob.SetValue<Number>(input);

			label = input.ToStringShort();
				
			return true;
		}
	}
}
﻿using System;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.Standard
{
	[Node (false, "Math/Negate")]
	public class NegateNode : Node 
	{
		public const string ID = "NegateNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Negate"; } }
		public override Vector2 DefaultSize { get { return new Vector2 (120, 50); } }
		public override string description { get { return "The negate node will return any given input in its negative equivalent."; } }


		[ValueConnectionKnob("Input", Direction.In, "Number")]
		public ValueConnectionKnob inputKnob;

		[ValueConnectionKnob("Output", Direction.Out, "Number")]
		public ValueConnectionKnob outputKnob;

		public Number input = new Number();

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
			input = 0f;

			if (inputKnob.connected()) {
				input = inputKnob.GetValue<Number>();
			}

			input = -input;

			outputKnob.SetValue<Number>(input);

			label = input.ToStringShort();
				
			return true;
		}
	}
}